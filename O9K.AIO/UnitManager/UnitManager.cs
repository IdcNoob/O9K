namespace O9K.AIO.UnitManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Heroes.Base;
    using Heroes.Dynamic.Units;

    using Modes.Combo;
    using Modes.MoveCombo;

    using SharpDX;

    using TargetManager;

    internal class UnitManager : IDisposable
    {
        protected readonly MultiSleeper abilitySleeper;

        protected readonly List<ControllableUnit> controllableUnits = new List<ControllableUnit>();

        protected readonly HashSet<string> ignoredUnits = new HashSet<string>
        {
            "npc_dota_juggernaut_healing_ward",
            "npc_dota_courier",
        };

        protected readonly Sleeper issuedAction = new Sleeper();

        protected readonly Dictionary<uint, float> issuedActionTimings = new Dictionary<uint, float>();

        protected readonly MultiSleeper orbwalkSleeper;

        protected readonly Owner owner;

        protected readonly TargetManager targetManager;

        protected readonly MultiSleeper unitIssuedAction = new MultiSleeper();

        protected readonly Dictionary<string, ControllableUnitMenu> unitMenus = new Dictionary<string, ControllableUnitMenu>();

        protected readonly Dictionary<string, Type> unitTypes = new Dictionary<string, Type>();

        protected bool controlAllies;

        public UnitManager(BaseHero baseHero)
        {
            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(ControllableUnit).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<UnitNameAttribute>())
                {
                    this.unitTypes.Add(attribute.Name, type);
                }
            }

            this.BaseHero = baseHero;
            this.targetManager = baseHero.TargetManager;
            this.owner = baseHero.Owner;
            this.abilitySleeper = baseHero.AbilitySleeper;
            this.orbwalkSleeper = baseHero.OrbwalkSleeper;

            this.Menu = baseHero.Menu.GeneralSettingsMenu.Add(new Menu("Units"));
            this.Menu.AddTranslation(Lang.Ru, "Юниты");
            this.Menu.AddTranslation(Lang.Cn, "单位");

            var control = this.Menu.Add(
                new MenuSwitcher("Control allies", "controlAllies", false).SetTooltip("Control disconnected/shared allies"));
            control.AddTranslation(Lang.Ru, "Управлять союзниками");
            control.AddTooltipTranslation(Lang.Ru, "Управлять отключившимеся/общими союзниками");
            control.AddTranslation(Lang.Cn, "控制盟友");
            control.AddTooltipTranslation(Lang.Cn, "控制断线/共享的盟友");

            control.ValueChange += this.ControlAlliesOnValueChanged;
        }

        public IEnumerable<ControllableUnit> AllControllableUnits
        {
            get
            {
                return this.controllableUnits.Where(x => x.IsValid);
            }
        }

        public BaseHero BaseHero { get; }

        public IEnumerable<ControllableUnit> ControllableUnits
        {
            get
            {
                return this.controllableUnits.Where(
                    x => x.IsValid && x.CanBeControlled && x.ShouldControl && x.Owner.Distance(this.owner) < 2000);
            }
        }

        public Menu Menu { get; }

        public void Disable()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            EntityManager9.UnitMonitor.AttackStart -= this.OnAttackStart;
            ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;

            foreach (var disposable in this.controllableUnits.OfType<IDisposable>())
            {
                disposable.Dispose();
            }

            this.controllableUnits.Clear();
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            EntityManager9.UnitMonitor.AttackStart -= this.OnAttackStart;
            ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;

            foreach (var disposable in this.controllableUnits.OfType<IDisposable>())
            {
                disposable.Dispose();
            }

            this.controllableUnits.Clear();
        }

        public void Enable()
        {
            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            EntityManager9.UnitMonitor.AttackStart += this.OnAttackStart;
            ObjectManager.OnAddTrackingProjectile += this.OnAddTrackingProjectile;
        }

        public void EndCombo(ComboModeMenu comboModeMenu)
        {
            foreach (var controllable in this.controllableUnits.Where(x => x.IsValid))
            {
                controllable.EndCombo(this.targetManager, comboModeMenu);
            }
        }

        public virtual void ExecuteCombo(ComboModeMenu comboModeMenu)
        {
            foreach (var controllable in this.ControllableUnits)
            {
                if (controllable.ComboSleeper.IsSleeping)
                {
                    continue;
                }

                if (!comboModeMenu.IgnoreInvisibility && controllable.IsInvisible)
                {
                    return;
                }

                if (controllable.Combo(this.targetManager, comboModeMenu))
                {
                    controllable.LastMovePosition = Vector3.Zero;
                }
            }
        }

        public void ExecuteMoveCombo(MoveComboModeMenu comboModeMenu)
        {
            foreach (var controllable in this.ControllableUnits)
            {
                if (controllable.ComboSleeper.IsSleeping)
                {
                    continue;
                }

                if (controllable.MoveCombo(this.targetManager, comboModeMenu))
                {
                    controllable.LastMovePosition = Vector3.Zero;
                    continue;
                }
            }
        }

        public void Move()
        {
            if (this.issuedAction.IsSleeping)
            {
                return;
            }

            var noOrbwalkUnits = new List<ControllableUnit>();

            foreach (var controllable in this.ControllableUnits.OrderBy(x => this.IssuedActionTime(x.Handle)))
            {
                if (!controllable.OrbwalkEnabled)
                {
                    noOrbwalkUnits.Add(controllable);
                    continue;
                }

                if (this.unitIssuedAction.IsSleeping(controllable.Handle))
                {
                    continue;
                }

                if (!controllable.Orbwalk(null, false, true))
                {
                    continue;
                }

                this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
                this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
                this.issuedAction.Sleep(0.08f);
                return;
            }

            if (noOrbwalkUnits.Count > 0 && !this.unitIssuedAction.IsSleeping(uint.MaxValue))
            {
                if (Player.EntitiesMove(noOrbwalkUnits.Where(x => x.Owner.CanMove()).Select(x => x.Owner.BaseUnit), Game.MousePosition))
                {
                    this.unitIssuedAction.Sleep(uint.MaxValue, 0.25f);
                    this.issuedAction.Sleep(0.08f);
                }
            }
        }

        public void Orbwalk(ControllableUnit controllable, bool attack, bool move)
        {
            if (this.issuedAction.IsSleeping)
            {
                return;
            }

            if (this.unitIssuedAction.IsSleeping(controllable.Handle))
            {
                return;
            }

            if (!controllable.Orbwalk(this.targetManager.Target, attack, move))
            {
                return;
            }

            this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
            this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
            this.issuedAction.Sleep(0.05f);
        }

        public virtual void Orbwalk(ComboModeMenu comboModeMenu)
        {
            if (this.issuedAction.IsSleeping)
            {
                return;
            }

            var allUnits = this.ControllableUnits.OrderBy(x => this.IssuedActionTime(x.Handle)).ToList();

            if (this.BodyBlock(allUnits, comboModeMenu))
            {
                this.issuedAction.Sleep(0.05f);
                return;
            }

            var noOrbwalkUnits = new List<ControllableUnit>();
            foreach (var controllable in allUnits)
            {
                if (!controllable.OrbwalkEnabled)
                {
                    noOrbwalkUnits.Add(controllable);
                    continue;
                }

                if (this.unitIssuedAction.IsSleeping(controllable.Handle))
                {
                    continue;
                }

                if (!controllable.Orbwalk(this.targetManager.Target, comboModeMenu))
                {
                    continue;
                }

                this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
                this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
                this.issuedAction.Sleep(0.05f);
                return;
            }

            if (noOrbwalkUnits.Count > 0 && !this.unitIssuedAction.IsSleeping(uint.MaxValue))
            {
                this.ControlAllUnits(noOrbwalkUnits);
            }
        }

        protected bool BodyBlock(ICollection<ControllableUnit> allUnits, ComboModeMenu comboModeMenu)
        {
            if (!this.targetManager.HasValidTarget)
            {
                return false;
            }

            var target = this.targetManager.Target;
            var bodyBlockUnits = this.ControllableUnits.Where(x => x.CanBodyBlock && x.Owner.Distance(target) < 1000)
                .OrderBy(x => x.Owner.Distance(target))
                .ToList();

            switch (Math.Min(bodyBlockUnits.Count, 2))
            {
                case 1:
                {
                    var controllable = bodyBlockUnits[0];

                    var bodyBlockResult = controllable.BodyBlock(this.targetManager, target.InFront(150), comboModeMenu);
                    // true - issued action, return
                    // false - already blocking, ignore
                    // null - let orbwalker control the unit

                    if (bodyBlockResult == true)
                    {
                        this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
                        this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
                        return true;
                    }

                    if (bodyBlockResult == false)
                    {
                        allUnits.Remove(controllable);
                    }

                    break;
                }
                case 2:
                {
                    var blockPositions = new List<Vector3>
                    {
                        target.InFront(150, 15),
                        target.InFront(150, -15)
                    };

                    foreach (var blockPosition in blockPositions.ToList())
                    {
                        var controllable = bodyBlockUnits.OrderBy(x => x.Owner.Distance(blockPosition)).First();

                        var swapControllable = bodyBlockUnits.Find(
                            x => !x.Equals(controllable) && blockPositions.Any(
                                     z => !z.Equals(blockPosition) && x.Owner.Distance(blockPosition) < x.Owner.Distance(z)));

                        if (swapControllable != null)
                        {
                            controllable = swapControllable;
                        }

                        if (this.unitIssuedAction.IsSleeping(controllable.Handle))
                        {
                            allUnits.Remove(controllable);
                            bodyBlockUnits.Remove(controllable);
                            blockPositions.Remove(blockPosition);
                            continue;
                        }

                        var bodyBlockResult = controllable.BodyBlock(this.targetManager, blockPosition, comboModeMenu);

                        if (bodyBlockResult == true)
                        {
                            this.issuedActionTimings[controllable.Handle] = Game.RawGameTime;
                            this.unitIssuedAction.Sleep(controllable.Handle, 0.2f);
                            return true;
                        }

                        if (bodyBlockResult == false)
                        {
                            allUnits.Remove(controllable);
                            bodyBlockUnits.Remove(controllable);
                            blockPositions.Remove(blockPosition);
                        }
                    }
                }
                    break;
            }

            return false;
        }

        protected void ControlAllUnits(IEnumerable<ControllableUnit> noOrbwalkUnits)
        {
            if (this.targetManager.HasValidTarget)
            {
                var units = noOrbwalkUnits.Where(x => x.Owner.CanAttack(this.targetManager.Target, x.CanMove() ? 999999 : 0))
                    .Select(x => x.Owner.BaseUnit)
                    .ToList();

                if (units.Count > 0)
                {
                    Player.EntitiesAttack(units, this.targetManager.Target.BaseUnit);
                }
            }
            else
            {
                var units = noOrbwalkUnits.Where(x => x.Owner.CanMove()).Select(x => x.Owner.BaseUnit).ToList();
                if (units.Count > 0)
                {
                    Player.EntitiesMove(units, Game.MousePosition);
                }
            }

            this.unitIssuedAction.Sleep(uint.MaxValue, 0.25f);
            this.issuedAction.Sleep(0.08f);
        }

        protected ControllableUnitMenu GetUnitMenu(Unit9 unit)
        {
            if (!this.unitMenus.TryGetValue(unit.DefaultName + unit.IsIllusion, out var menu))
            {
                menu = new ControllableUnitMenu(unit, this.Menu);
                this.unitMenus[unit.DefaultName + unit.IsIllusion] = menu;
            }

            return menu;
        }

        protected float IssuedActionTime(uint handle)
        {
            this.issuedActionTimings.TryGetValue(handle, out var time);
            return time;
        }

        protected virtual void OnAbilityAdded(Ability9 entity)
        {
            try
            {
                if (!entity.IsControllable || entity.IsFake || !entity.Owner.IsAlly(this.owner.Team)
                    || !(entity is ActiveAbility activeAbility))
                {
                    return;
                }

                var controllable = this.controllableUnits.Find(x => x.Handle == entity.Owner.Handle);
                controllable?.AddAbility(activeAbility, this.BaseHero.ComboMenus, this.BaseHero.MoveComboModeMenu);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        protected virtual void OnAbilityRemoved(Ability9 entity)
        {
            try
            {
                if (!entity.IsControllable || entity.IsFake || !entity.Owner.IsAlly(this.owner.Team)
                    || !(entity is ActiveAbility activeAbility))
                {
                    return;
                }

                var controllable = this.controllableUnits.Find(x => x.Handle == entity.Owner.Handle);
                controllable?.RemoveAbility(activeAbility);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ControlAlliesOnValueChanged(object sender, SwitcherEventArgs e)
        {
            this.controlAllies = e.NewValue;

            foreach (var unit in EntityManager9.Units)
            {
                this.OnUnitRemoved(unit);
                this.OnUnitAdded(unit);
            }

            foreach (var ability in EntityManager9.Abilities)
            {
                this.OnAbilityAdded(ability);
                this.OnAbilityRemoved(ability);
            }
        }

        private void OnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            try
            {
                var source = args.Projectile.Source;
                if (source?.IsValid != true)
                {
                    return;
                }

                var controllable = this.controllableUnits.Find(x => x.Handle == source.Handle);
                if (controllable == null)
                {
                    return;
                }

                controllable.MoveSleeper.Reset();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAttackStart(Unit9 unit)
        {
            try
            {
                if (!unit.IsControllable)
                {
                    return;
                }

                var controllable = this.controllableUnits.Find(x => x.Handle == unit.Handle);
                controllable?.OnAttackStart();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitAdded(Unit9 entity)
        {
            try
            {
                if (!entity.IsControllable || !entity.IsAlly(this.owner.Team))
                {
                    return;
                }

                if (this.ignoredUnits.Contains(entity.Name) || this.controllableUnits.Any(x => x.Handle == entity.Handle))
                {
                    return;
                }

                if (!this.controlAllies)
                {
                    if (entity.IsHero)
                    {
                        if (entity.BaseOwner?.Handle != this.owner.PlayerHandle)
                        {
                            return;
                        }
                    }
                    else if (entity.BaseOwner?.Handle != this.owner.HeroHandle)
                    {
                        return;
                    }
                }

                if (!entity.CanUseAbilities || !this.unitTypes.TryGetValue(entity.Name, out var type))
                {
                    if (entity.CanUseAbilities)
                    {
                        var d = new DynamicUnit(
                            entity,
                            this.abilitySleeper,
                            this.orbwalkSleeper[entity.Handle],
                            this.GetUnitMenu(entity),
                            this.BaseHero)
                        {
                            FailSafe = this.BaseHero.FailSafe
                        };

                        this.controllableUnits.Add(d);
                    }
                    else
                    {
                        var c = new ControllableUnit(
                            entity,
                            this.abilitySleeper,
                            this.orbwalkSleeper[entity.Handle],
                            this.GetUnitMenu(entity))
                        {
                            FailSafe = this.BaseHero.FailSafe
                        };

                        this.controllableUnits.Add(c);
                    }

                    return;
                }

                var ac = (ControllableUnit)Activator.CreateInstance(
                    type,
                    entity,
                    this.abilitySleeper,
                    this.orbwalkSleeper[entity.Handle],
                    this.GetUnitMenu(entity));
                ac.FailSafe = this.BaseHero.FailSafe;

                this.controllableUnits.Add(ac);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitRemoved(Unit9 entity)
        {
            try
            {
                if (!entity.IsControllable || !entity.IsAlly(this.owner.Team) || !entity.IsUnit)
                {
                    return;
                }

                var unit = this.controllableUnits.Find(x => x.Handle == entity.Handle);
                if (unit == null)
                {
                    return;
                }

                if (unit is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                this.controllableUnits.Remove(unit);
                this.issuedActionTimings.Remove(entity.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}