namespace O9K.Core.Managers.Entity.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Entities.Abilities.Base.Components;
    using Entities.Abilities.Base.Types;
    using Entities.Heroes;
    using Entities.Heroes.Unique;
    using Entities.Units;

    using Helpers;
    using Helpers.Damage;
    using Helpers.Range;

    using Logger;

    public sealed class UnitMonitor : IDisposable
    {
        private readonly Team allyTeam;

        private readonly HashSet<int> attackActivities = new HashSet<int>
        {
            (int)NetworkActivity.Attack,
            (int)NetworkActivity.Attack2,
            (int)NetworkActivity.Crit,
            (int)NetworkActivity.AttackEvent,
            (int)NetworkActivity.AttackEventBash
        };

        private readonly HashSet<string> attackAnimation = new HashSet<string>
        {
            "tidebringer",
            "impetus_anim"
        };

        private readonly MultiSleeper attackSleeper = new MultiSleeper();

        private readonly DamageFactory damageFactory;

        private readonly HashSet<string> notAttackAnimation = new HashSet<string>
        {
            "sniper_attack_schrapnel_cast1_aggressive",
            "sniper_attack_schrapnel_cast1_aggressive_anim",
            "attack_omni_cast",
            "lotfl_dualwield_press_the_attack",
            "lotfl_press_the_attack",
            "sniper_attack_assassinate_dreamleague",
        };

        private readonly RangeFactory rangeFactory;

        private readonly Dictionary<string, Action<Unit9, Modifier, bool>> specialModifiers =
            new Dictionary<string, Action<Unit9, Modifier, bool>>
            {
                { "modifier_teleporting", (x, _, value) => x.IsTeleporting = value },
                { "modifier_riki_permanent_invisibility", (u, _, value) => u.CanUseAbilitiesInInvisibility = value },
                { "modifier_ice_blast", (x, _, value) => x.CanBeHealed = !value },
                { "modifier_item_aegis", (x, _, value) => x.HasAegis = value },
                { "modifier_necrolyte_sadist_active", (x, _, value) => x.IsEthereal = value },
                { "modifier_pugna_decrepify", (x, _, value) => x.IsEthereal = value },
                { "modifier_item_ethereal_blade_ethereal", (x, _, value) => x.IsEthereal = value },
                { "modifier_ghost_state", (x, _, value) => x.IsEthereal = value },
                { "modifier_item_lotus_orb_active", (x, _, value) => x.IsLotusProtected = value },
                { "modifier_antimage_counterspell", (x, _, value) => x.IsSpellShieldProtected = value },
                { "modifier_item_sphere_target", (x, _, value) => x.IsLinkensTargetProtected = value },
                { "modifier_item_blade_mail_reflect", (x, _, value) => x.IsReflectingDamage = value },
                { "modifier_item_ultimate_scepter_consumed", (x, _, __) => x.HasAghanimsScepterBlessing = true },
                { "modifier_slark_dark_pact", (x, _, value) => x.IsDarkPactProtected = value },
                { "modifier_bloodseeker_rupture", (x, _, value) => x.IsRuptured = value },
                { "modifier_spirit_breaker_charge_of_darkness", (x, _, value) => x.IsCharging = value },
                { "modifier_dragon_knight_dragon_form", (x, _, value) => x.IsRanged = value || x.BaseUnit.IsRanged },
                { "modifier_terrorblade_metamorphosis", (x, _, value) => x.IsRanged = value || x.BaseUnit.IsRanged },
                { "modifier_troll_warlord_berserkers_rage", (x, _, value) => x.IsRanged = !value || x.BaseUnit.IsRanged },
                { "modifier_lone_druid_true_form", (x, _, value) => x.IsRanged = !value || x.BaseUnit.IsRanged },
                {
                    "modifier_brewmaster_primal_split", (x, mod, value) =>
                        {
                            if (value)
                            {
                                x.HideHud = true;
                                x.ForceUnitState(UnitState.Unselectable | UnitState.CommandRestricted, mod.RemainingTime);
                            }
                            else
                            {
                                x.HideHud = false;
                            }
                        }
                },
                {
                    "modifier_slark_shadow_dance_visual", (x, _, value) =>
                        {
                            var slark = x.Owner as Slark;
                            slark?.ShadowDanced(value);
                        }
                },
                {
                    "modifier_morphling_replicate", (x, _, value) =>
                        {
                            var morphling = x as Morphling;
                            morphling?.Morphed(value);
                        }
                },
                {
                    "modifier_alchemist_chemical_rage", (x, _, value) =>
                        {
                            var alchemist = x as Alchemist;
                            alchemist?.Raged(value);
                        }
                }
            };

        public UnitMonitor()
        {
            this.allyTeam = EntityManager9.Owner.Team;
            this.damageFactory = new DamageFactory();
            this.rangeFactory = new RangeFactory();

            Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
            Entity.OnAnimationChanged += this.OnAnimationChanged;
            Unit.OnModifierAdded += this.OnModifierAdded;
            Unit.OnModifierRemoved += this.OnModifierRemoved;
            Entity.OnHandlePropertyChange += this.OnHandlePropertyChange;
            Game.OnFireEvent += this.OnFireEvent;
            Entity.OnBoolPropertyChange += OnBoolPropertyChange;
            Player.OnExecuteOrder += OnExecuteOrder;
            Entity.OnInt64PropertyChange += OnInt64PropertyChange;

            Drawing.OnDraw += OnUpdateDraw;
            UpdateManager.Subscribe(OnUpdate);
        }

        public delegate void EventHandler(Unit9 unit);

        public delegate void HealthEventHandler(Unit9 unit, float health);

        public event EventHandler AttackEnd;

        public event EventHandler AttackStart;

        public event EventHandler UnitDied;

        public event HealthEventHandler UnitHealthChange;

        public void Dispose()
        {
            this.damageFactory.Dispose();
            this.rangeFactory.Dispose();

            Entity.OnInt64PropertyChange -= OnInt64PropertyChange;
            Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
            Entity.OnAnimationChanged -= this.OnAnimationChanged;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            Player.OnExecuteOrder -= OnExecuteOrder;

            Drawing.OnDraw -= OnUpdateDraw;
        }

        internal void CheckModifiers(Unit9 unit)
        {
            try
            {
                foreach (var modifier in unit.BaseModifiers)
                {
                    this.CheckModifier(unit.Handle, modifier, true);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void DropTarget(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var unit = EntityManager9.GetUnitFast(entity.Handle);
                if (unit != null)
                {
                    unit.Target = null;
                    unit.IsAttacking = false;
                }
            }
        }

        private static void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            if (args.NewValue == args.OldValue)
            {
                return;
            }

            try
            {
                switch (args.PropertyName)
                {
                    case "m_bReincarnating":
                    {
                        var unit = (Hero9)EntityManager9.GetUnitFast(sender.Handle);
                        if (unit == null)
                        {
                            return;
                        }

                        unit.IsReincarnating = args.NewValue;
                        break;
                    }
                    //case "m_bIsWaitingToSpawn":
                    //{
                    //    if (args.NewValue)
                    //    {
                    //        return;
                    //    }

                    //    var unit = EntityManager9.GetUnitFast(sender.Handle);
                    //    if (unit == null)
                    //    {
                    //        return;
                    //    }

                    //    unit.IsSpawned = true;
                    //    break;
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (args.IsQueued || !args.Process)
            {
                return;
            }

            try
            {
                switch (args.OrderId)
                {
                    case OrderId.AttackTarget:
                    {
                        SetTarget(args.Entities, args.Target.Handle);
                        break;
                    }

                    case OrderId.Hold:
                    case OrderId.Stop:
                    {
                        DropTarget(args.Entities);
                        StopChanneling(args.Entities);
                        break;
                    }

                    case OrderId.MoveLocation:
                    case OrderId.MoveTarget:
                    {
                        DropTarget(args.Entities);
                        break;
                    }

                    case OrderId.AbilityTarget:
                    {
                        var target = EntityManager9.GetUnitFast(args.Target.Handle);
                        if (target?.IsLinkensProtected == true)
                        {
                            return;
                        }

                        StartChanneling(args.Ability.Handle);
                        break;
                    }

                    case OrderId.Ability:
                    case OrderId.AbilityLocation:
                    {
                        StartChanneling(args.Ability.Handle);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void OnInt64PropertyChange(Entity sender, Int64PropertyChangeEventArgs args)
        {
            if (args.NewValue == args.OldValue)
            {
                return;
            }

            if (args.PropertyName == "m_iIsControllableByPlayer64")
            {
                UpdateManager.BeginInvoke(() => EntityManager9.ChangeEntityControl(sender));
            }
        }

        private static void OnUpdate()
        {
            foreach (var ability in EntityManager9.abilitiesList)
            {
                try
                {
                    if (!ability.IsValid)
                    {
                        continue;
                    }

                    ability.IsValid = ability.BaseAbility.IsValid;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }

            foreach (var unit in EntityManager9.unitsList)
            {
                try
                {
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    if (!(unit.IsValid = unit.BaseUnit.IsValid))
                    {
                        continue;
                    }

                    if (!unit.IsVisible)
                    {
                        unit.LastNotVisibleTime = Game.RawGameTime;
                        continue;
                    }

                    unit.LastVisibleTime = Game.RawGameTime;

                    var baseUnit = unit.BaseUnit;

                    if (!(unit.BaseIsAlive = baseUnit.IsAlive))
                    {
                        continue;
                    }

                    unit.BaseHealth = baseUnit.Health;

                    if (unit.IsBuilding)
                    {
                        continue;
                    }

                    unit.BaseMana = baseUnit.Mana;

                    try
                    {
                        unit.Speed = baseUnit.MovementSpeed;
                    }
                    catch (DivideByZeroException)
                    {
                        //hack ensage core bug
                        unit.Speed = 0;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private static void OnUpdateDraw(EventArgs args)
        {
            foreach (var unit in EntityManager9.unitsList)
            {
                try
                {
                    if (!unit.IsValid)
                    {
                        continue;
                    }

                    if (unit.IsBuilding)
                    {
                        continue;
                    }

                    if (!(unit.IsVisible = unit.BaseUnit.IsVisible))
                    {
                        continue;
                    }

                    unit.LastPositionUpdateTime = Game.RawGameTime;
                    unit.BasePosition = unit.BaseUnit.Position;
                }
                catch (EntityNotFoundException)
                {
                    // ignore
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private static void RespawnUnit(Unit9 unit)
        {
            try
            {
                if (!unit.IsValid || unit.IsVisible || unit.BaseIsAlive)
                {
                    return;
                }

                unit.BaseIsAlive = true;
                unit.BaseHealth = unit.MaximumHealth;
                unit.BaseMana = unit.MaximumMana;
                unit.BasePosition = EntityManager9.EnemyFountain;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void SetTarget(IEnumerable<Entity> entities, uint targetHandle)
        {
            var target = EntityManager9.GetUnit(targetHandle);
            if (target?.IsAlive != true)
            {
                return;
            }

            foreach (var entity in entities)
            {
                var unit = EntityManager9.GetUnitFast(entity.Handle);
                if (unit == null)
                {
                    continue;
                }

                unit.Target = target;
                unit.IsAttacking = true;
            }
        }

        private static void StartChanneling(uint abilityHandle)
        {
            if (!(EntityManager9.GetAbilityFast(abilityHandle) is IChanneled channeled))
            {
                return;
            }

            channeled.Owner.ChannelEndTime = Game.RawGameTime + channeled.GetCastDelay() + 1f;
        }

        private static void StopChanneling(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var unit = EntityManager9.GetUnitFast(entity.Handle);
                if (unit == null)
                {
                    continue;
                }

                unit.ChannelEndTime = 0;
                unit.ChannelActivatesOnCast = false;
            }
        }

        private void AttackStarted(Unit9 unit)
        {
            if (this.attackSleeper.IsSleeping(unit.Handle) || !unit.IsAlive)
            {
                return;
            }

            if (!unit.IsControllable && unit.IsHero)
            {
                unit.Target = EntityManager9.Units
                    .Where(x => x.IsAlive && !x.IsAlly(unit) && x.Distance(unit) <= unit.GetAttackRange(x, 25))
                    .OrderBy(x => unit.GetAngle(x.Position))
                    .FirstOrDefault(x => unit.GetAngle(x.Position) < 0.35f);
            }

            unit.IsAttacking = true;
            this.attackSleeper.Sleep(unit.Handle, unit.GetAttackPoint());

            this.AttackStart?.Invoke(unit);
        }

        private void AttackStopped(Unit9 unit)
        {
            if (!this.attackSleeper.IsSleeping(unit.Handle))
            {
                return;
            }

            if (!unit.IsControllable && !unit.IsTower && unit.IsHero)
            {
                unit.Target = null;
            }

            unit.IsAttacking = false;
            this.attackSleeper.Reset(unit.Handle);

            this.AttackEnd?.Invoke(unit);
        }

        private void CheckModifier(uint senderHandle, Modifier modifier, bool added)
        {
            var modifierName = modifier.Name;
            var unit = EntityManager9.GetUnitFast(senderHandle);

            if (unit == null)
            {
                return;
            }

            if (this.specialModifiers.TryGetValue(modifierName, out var action))
            {
                action(unit, modifier, added);
            }

            var range = this.rangeFactory.GetRange(modifierName);
            if (range != null)
            {
                unit.Range(range, added);
                return;
            }

            if (modifier.IsHidden)
            {
                return;
            }

            var disable = this.damageFactory.GetDisable(modifierName);
            if (disable != null)
            {
                var invulnerability = disable is IDisable disableAbility && (disableAbility.AppliesUnitState & UnitState.Invulnerable) != 0;
                unit.Disabler(modifier, added, invulnerability);
                return;
            }

            if (modifier.IsStunDebuff)
            {
                unit.Disabler(modifier, added, false);
                return;
            }

            var amplifier = this.damageFactory.GetAmplifier(modifierName);
            if (amplifier != null)
            {
                unit.Amplifier(amplifier, added);
                return;
            }

            var passive = this.damageFactory.GetPassive(modifierName);
            if (passive != null)
            {
                unit.Passive(passive, added);
                return;
            }

            var blocker = this.damageFactory.GetBlocker(modifierName);
            if (blocker != null)
            {
                unit.Blocker(blocker, added);
                return;
            }
        }

        private bool IsAttackAnimation(Animation animation)
        {
            var name = animation.Name;

            if (this.notAttackAnimation.Contains(name))
            {
                return false;
            }

            if (this.attackAnimation.Contains(name))
            {
                return true;
            }

            if (name.IndexOf("attack", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                this.attackAnimation.Add(name);
                return true;
            }

            this.notAttackAnimation.Add(name);
            return false;
        }

        private void OnAnimationChanged(Entity sender, EventArgs args)
        {
            try
            {
                var unit = EntityManager9.GetUnit(sender.Handle);
                if (unit == null)
                {
                    return;
                }

                if (this.IsAttackAnimation(sender.Animation))
                {
                    this.AttackStarted(unit);
                }
                else
                {
                    this.AttackStopped(unit);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnFireEvent(FireEventEventArgs args)
        {
            try
            {
                switch (args.GameEvent.Name)
                {
                    case "dota_player_kill":
                    case "dota_player_deny":
                    {
                        var id = (uint)args.GameEvent.GetInt("victim_userid");
                        var handle = ObjectManager.GetPlayerById(id)?.Hero?.Handle;
                        var unit = (Hero9)EntityManager9.GetUnitFast(handle);

                        if (unit == null || unit.Team == this.allyTeam)
                        {
                            return;
                        }

                        unit.BaseIsAlive = false;

                        if (unit.IsVisible)
                        {
                            var delay = (int)(((unit.BaseHero.RespawnTime - Game.RawGameTime) + 0.5f) * 1000);
                            if (delay <= 0)
                            {
                                return;
                            }

                            UpdateManager.BeginInvoke(() => RespawnUnit(unit), delay);
                        }

                        break;
                    }
                    case "dota_buyback":
                    {
                        var id = (uint)args.GameEvent.GetInt("player_id");
                        var handle = ObjectManager.GetPlayerById(id)?.Hero?.Handle;
                        var unit = EntityManager9.GetUnitFast(handle);

                        if (unit == null || unit.Team == this.allyTeam)
                        {
                            return;
                        }

                        unit.BaseIsAlive = true;
                        unit.BaseHealth = unit.MaximumHealth;
                        unit.BaseMana = unit.MaximumMana;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnHandlePropertyChange(Entity sender, HandlePropertyChangeEventArgs args)
        {
            if (sender.Team == this.allyTeam || args.NewValue == args.OldValue || !args.OldValue.IsValid
                || args.PropertyName != "m_hKillCamUnit")
            {
                return;
            }

            try
            {
                // respawn
                var handle = ((Player)sender).Hero?.Handle;
                var unit = (Hero9)EntityManager9.GetUnitFast(handle);

                if (unit == null)
                {
                    return;
                }

                UpdateManager.BeginInvoke(() => RespawnUnit(unit), 500);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            var newValue = args.NewValue;
            if (newValue == args.OldValue)
            {
                return;
            }

            try
            {
                var unit = EntityManager9.GetUnitFast(sender.Handle);
                if (unit == null)
                {
                    return;
                }

                switch (args.PropertyName)
                {
                    case "m_iHealth":
                    {
                        if (newValue > 0)
                        {
                            this.UnitHealthChange?.Invoke(unit, newValue);
                            break;
                        }

                        unit.DeathTime = Game.RawGameTime;

                        this.AttackStopped(unit);
                        this.attackSleeper.Remove(unit.Handle);

                        this.UnitDied?.Invoke(unit);
                        break;
                    }

                    case "m_NetworkActivity":
                    {
                        if (this.attackActivities.Contains(newValue))
                        {
                            this.AttackStarted(unit);
                        }
                        else
                        {
                            this.AttackStopped(unit);
                        }

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, sender);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                this.CheckModifier(sender.Handle, args.Modifier, true);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                this.CheckModifier(sender.Handle, args.Modifier, false);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}