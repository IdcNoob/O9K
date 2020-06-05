namespace O9K.AIO.KillStealer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Abilities.Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Exceptions;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Helpers.Damage;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Heroes.Base;

    using Modes.Base;

    using SharpDX;

    using TargetManager;

    internal class KillSteal : BaseMode
    {
        private readonly Dictionary<AbilityId, Type> abilityTypes = new Dictionary<AbilityId, Type>();

        private readonly IUpdateHandler damageHandler;

        private readonly HashSet<AbilityId> highPriorityKillSteal = new HashSet<AbilityId>
        {
            AbilityId.morphling_adaptive_strike_agi,
            AbilityId.tusk_walrus_punch,
        };

        private readonly HashSet<AbilityId> ignoredAmplifiers = new HashSet<AbilityId>
        {
            AbilityId.winter_wyvern_winters_curse,
            AbilityId.centaur_stampede,
            AbilityId.kunkka_ghostship,
            AbilityId.legion_commander_duel,
            AbilityId.medusa_stone_gaze,
            AbilityId.nyx_assassin_spiked_carapace,
        };

        private readonly IUpdateHandler killStealHandler;

        private readonly MultiSleeper orbwalkSleeper;

        private readonly TargetManager targetManager;

        private List<KillStealAbility> activeAbilities = new List<KillStealAbility>();

        private Dictionary<Unit9, Dictionary<KillStealAbility, int>> targetDamage =
            new Dictionary<Unit9, Dictionary<KillStealAbility, int>>();

        public KillSteal(BaseHero baseHero)
            : base(baseHero)
        {
            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(KillStealAbility).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<AbilityIdAttribute>())
                {
                    this.abilityTypes.Add(attribute.AbilityId, type);
                }
            }

            this.KillStealMenu = new KillStealMenu(baseHero.Menu.RootMenu);
            this.orbwalkSleeper = baseHero.OrbwalkSleeper;
            this.targetManager = baseHero.TargetManager;

            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            EntityManager9.AbilityRemoved += this.OnAbilityRemoved;

            this.damageHandler = UpdateManager.Subscribe(this.OnUpdateDamage, 200, false);
            this.killStealHandler = UpdateManager.Subscribe(this.OnUpdateKillSteal, 0, false);

            this.KillStealMenu.OverlayX.ValueChange += this.OverlayXOnValueChanged;
            this.KillStealMenu.OverlayY.ValueChange += this.OverlayYOnValueChanged;
            this.KillStealMenu.OverlaySizeX.ValueChange += this.OverlaySizeXOnValueChanged;
            this.KillStealMenu.OverlaySizeY.ValueChange += this.OverlaySizeYOnValueChanged;
        }

        public MultiSleeper AbilitySleeper { get; } = new MultiSleeper();

        public Vector2 AdditionalOverlayPosition { get; private set; }

        public Vector2 AdditionalOverlaySize { get; private set; }

        public KillStealMenu KillStealMenu { get; }

        public Sleeper KillStealSleeper { get; } = new Sleeper();

        public Unit9 Target { get; private set; }

        public void Disable()
        {
            this.KillStealMenu.KillStealEnabled.ValueChange -= this.KillStealEnabledOnValueChanged;
            this.KillStealMenu.OverlayEnabled.ValueChange -= this.OverlayEnabledOnValueChanged;

            this.KillStealEnabledOnValueChanged(null, new SwitcherEventArgs(false, false));
            this.OverlayEnabledOnValueChanged(null, new SwitcherEventArgs(false, false));
        }

        public override void Dispose()
        {
            Drawing.OnDraw -= this.OnDraw;
            UpdateManager.Unsubscribe(this.damageHandler);
            UpdateManager.Unsubscribe(this.OnUpdateKillSteal);
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            this.KillStealMenu.KillStealEnabled.ValueChange -= this.KillStealEnabledOnValueChanged;
            this.KillStealMenu.OverlayEnabled.ValueChange -= this.OverlayEnabledOnValueChanged;
            this.KillStealMenu.OverlayX.ValueChange -= this.OverlayXOnValueChanged;
            this.KillStealMenu.OverlayY.ValueChange -= this.OverlayYOnValueChanged;
            this.KillStealMenu.OverlaySizeX.ValueChange -= this.OverlaySizeXOnValueChanged;
            this.KillStealMenu.OverlaySizeY.ValueChange -= this.OverlaySizeYOnValueChanged;
        }

        public void Enable()
        {
            this.KillStealMenu.KillStealEnabled.ValueChange += this.KillStealEnabledOnValueChanged;
            this.KillStealMenu.OverlayEnabled.ValueChange += this.OverlayEnabledOnValueChanged;
        }

        private void AddAbility(ActiveAbility ability)
        {
            if (this.abilityTypes.TryGetValue(ability.Id, out var type))
            {
                this.activeAbilities.Add((KillStealAbility)Activator.CreateInstance(type, ability));
            }
            else
            {
                this.activeAbilities.Add(new KillStealAbility(ability));
            }

            this.activeAbilities = this.activeAbilities.OrderByDescending(x => x.Ability is IHasDamageAmplify && !(x.Ability is INuke))
                .ThenByDescending(x => x.Ability is IHasDamageAmplify && x.Ability is INuke)
                .ThenByDescending(x => this.highPriorityKillSteal.Contains(x.Ability.Id))
                .ThenBy(x => x.Ability.CastPoint)
                .ToList();
        }

        private Dictionary<KillStealAbility, int> GetDamage(Unit9 target)
        {
            var abilities = new List<KillStealAbility>();
            var additionalAmplifiers = new DamageAmplifier();
            var damageBlockers = target.GetDamageBlockers().ToDictionary(x => x, x => x.BlockValue(target));

            foreach (var ability in this.activeAbilities.Where(x => this.KillStealMenu.IsAbilityEnabled(x.Ability.Name)))
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (!this.AbilitySleeper.IsSleeping(ability.Ability.Handle) && !ability.Ability.CanBeCasted(false))
                {
                    continue;
                }

                //todo remove positive amplifiers
                if (ability.Ability is IHasDamageAmplify amplifier)
                {
                    if ((amplifier.IsIncomingDamageAmplifier() && !target.HasModifier(amplifier.AmplifierModifierNames))
                        || (amplifier.IsOutgoingDamageAmplifier() && !ability.Ability.Owner.HasModifier(amplifier.AmplifierModifierNames)))
                    {
                        if (amplifier.IsPhysicalDamageAmplifier())
                        {
                            additionalAmplifiers[DamageType.Physical] *= 1 + amplifier.AmplifierValue(ability.Ability.Owner, target);
                        }

                        if (amplifier.IsMagicalDamageAmplifier())
                        {
                            additionalAmplifiers[DamageType.Magical] *= 1 + amplifier.AmplifierValue(ability.Ability.Owner, target);
                        }

                        if (amplifier.IsPureDamageAmplifier())
                        {
                            additionalAmplifiers[DamageType.Pure] *= 1 + amplifier.AmplifierValue(ability.Ability.Owner, target);
                        }
                    }
                }

                abilities.Add(ability);
            }

            var remainingHealth = target.Health;
            var finalDamage = new Dictionary<KillStealAbility, int>();
            var remainingMana = new Dictionary<Unit9, float>();

            foreach (var ability in abilities)
            {
                if (!remainingMana.TryGetValue(ability.Ability.Owner, out var mana))
                {
                    remainingMana[ability.Ability.Owner] = mana = ability.Ability.Owner.Mana;
                }

                if (mana < ability.Ability.ManaCost)
                {
                    continue;
                }

                foreach (var damagePair in ability.Ability.GetRawDamage(target, remainingHealth))
                {
                    var damageType = damagePair.Key;
                    var damage = damagePair.Value;

                    if (damageType == DamageType.None)
                    {
                        if (damage > 0)
                        {
                            var ex = new BrokenAbilityException("DamageType");
                            Logger.Error(ex, ability.Ability.Name);
                            this.activeAbilities.RemoveAll(x => x.Ability.Handle == ability.Ability.Handle);
                        }

                        finalDamage.Add(ability, 0);
                        continue;
                    }

                    if (damageType == DamageType.HealthRemoval)
                    {
                        finalDamage.TryGetValue(ability, out var hpRemovalDamage);
                        finalDamage[ability] = hpRemovalDamage + (int)damage;
                        remainingHealth -= damage;
                        continue;
                    }

                    var amp = target.GetDamageAmplification(ability.Ability.Owner, damageType, ability.Ability.IntelligenceAmplify);
                    if (amp <= 0)
                    {
                        continue;
                    }

                    foreach (var damageBlock in damageBlockers.ToList())
                    {
                        var blockAbility = damageBlock.Key;
                        if (!blockAbility.CanBlock(damageType))
                        {
                            continue;
                        }

                        var blockedDamage = Math.Min(damage, damageBlock.Value);
                        damageBlockers[blockAbility] -= blockedDamage;

                        if (blockAbility.BlocksDamageAfterReduction)
                        {
                            damage -= Math.Min(damage, blockedDamage * (1 / (amp * additionalAmplifiers[damageType])));
                        }
                        else
                        {
                            damage -= blockedDamage;
                        }
                    }

                    var amplifiedDamage = (int)(damage * amp * additionalAmplifiers[damageType]);
                    if (amplifiedDamage <= 0)
                    {
                        continue;
                    }

                    remainingMana[ability.Ability.Owner] -= ability.Ability.ManaCost;
                    finalDamage.TryGetValue(ability, out var savedDamage);
                    finalDamage[ability] = savedDamage + amplifiedDamage;
                    remainingHealth -= amplifiedDamage;
                }
            }

            return finalDamage;
        }

        private void Kill(Unit9 target, IReadOnlyList<KillStealAbility> abilities)
        {
            //todo dynamic order

            if (this.KillStealSleeper.IsSleeping)
            {
                return;
            }

            foreach (var ability in abilities.OrderBy(x => x.Ability.GetDamage(target)))
            {
                var hitTime = ability.Ability.GetHitTime(target);

                if (target.Health + (target.HealthRegeneration * hitTime * 1.5f) > ability.Ability.GetDamage(target))
                {
                    continue;
                }

                if (!ability.UseAbility(target))
                {
                    continue;
                }

                var castDelay = ability.Ability.GetCastDelay(target);
                this.AbilitySleeper.Sleep(ability.Ability.Handle, hitTime);
                this.orbwalkSleeper.Sleep(ability.Ability.Owner.Handle, castDelay);
                this.KillStealSleeper.Sleep(hitTime - ability.Ability.ActivationDelay);
                return;
            }

            for (var i = 0; i < abilities.Count; i++)
            {
                var ability = abilities[i];

                if (this.orbwalkSleeper.IsSleeping(ability.Ability.Owner.Handle))
                {
                    return;
                }

                if (!ability.Ability.CanBeCasted())
                {
                    continue;
                }

                var amplifier = ability.Ability as IHasDamageAmplify;
                var ampsDamage = amplifier?.IsIncomingDamageAmplifier() == true;
                var targetAmplified = ampsDamage && target.HasModifier(amplifier.AmplifierModifierNames);

                if (targetAmplified && !(ability.Ability is INuke))
                {
                    continue;
                }

                if (!ability.UseAbility(target))
                {
                    return;
                }

                float delay;
                if (!targetAmplified)
                {
                    var nextAbility = i < abilities.Count - 1 ? abilities[i + 1] : null;
                    if (nextAbility != null)
                    {
                        var nextAbilityDelay = nextAbility.Ability.GetHitTime(target);
                        var ampDelay = ability.Ability.GetHitTime(target);
                        delay = ampDelay - nextAbilityDelay;
                    }
                    else
                    {
                        delay = ability.Ability.GetHitTime(target);
                    }
                }
                else
                {
                    delay = ability.Ability.GetCastDelay(target);
                }

                this.AbilitySleeper.Sleep(ability.Ability.Handle, ability.Ability.GetHitTime(target));
                this.orbwalkSleeper.Sleep(ability.Ability.Owner.Handle, ability.Ability.GetCastDelay(target));
                this.KillStealSleeper.Sleep(delay - ability.Ability.ActivationDelay);
                return;
            }
        }

        private void KillStealEnabledOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.killStealHandler.IsEnabled = true;
                this.damageHandler.IsEnabled = true;
            }
            else
            {
                this.killStealHandler.IsEnabled = false;
                if (!this.KillStealMenu.OverlayEnabled)
                {
                    this.damageHandler.IsEnabled = false;
                }
            }
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (!ability.IsControllable || ability.IsFake || !ability.Owner.IsAlly(this.Owner.Team) || !ability.Owner.IsMyControllable)
                {
                    return;
                }

                if (this.ignoredAmplifiers.Contains(ability.Id) || !(ability is ActiveAbility active))
                {
                    return;
                }

                if (ability is IHasDamageAmplify amplifier && ((amplifier.IsIncomingDamageAmplifier() && active.TargetsEnemy)
                                                               || (amplifier.IsOutgoingDamageAmplifier() && active.TargetsAlly)))
                {
                    this.AddAbility(active);
                    this.KillStealMenu.AddKillStealAbility(active);
                }
                else if (ability is INuke)
                {
                    this.AddAbility(active);
                    this.KillStealMenu.AddKillStealAbility(active);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (!ability.IsControllable || ability.IsFake || !ability.Owner.IsAlly(this.Owner.Team))
                {
                    return;
                }

                if (!(ability is ActiveAbility))
                {
                    return;
                }

                this.activeAbilities.RemoveAll(x => x.Ability.Handle == ability.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(EventArgs args)
        {
            try
            {
                foreach (var damagePair in this.targetDamage)
                {
                    var damage = damagePair.Value.Sum(x => x.Value);
                    if (damage <= 0)
                    {
                        continue;
                    }

                    var hero = damagePair.Key;
                    if (!hero.IsValid || !hero.IsAlive || hero.IsInvulnerable || !hero.IsVisible)
                    {
                        continue;
                    }

                    var hpPosition = hero.HealthBarPosition;
                    if (hpPosition.IsZero)
                    {
                        continue;
                    }

                    var health = hero.Health;
                    var damagePercentage = Math.Min(damage, health) / hero.MaximumHealth;
                    var healthBarSize = hero.HealthBarSize;
                    var start = hpPosition + new Vector2(0, healthBarSize.Y * 0.7f) + this.AdditionalOverlayPosition;
                    var size = (healthBarSize * new Vector2(damagePercentage, 0.4f)) + this.AdditionalOverlaySize;

                    Drawing.DrawRect(start, size, damage >= health ? Color.LightGreen : Color.DarkOliveGreen);
                    Drawing.DrawRect(start - new Vector2(1), size + new Vector2(1), Color.Black, true);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdateDamage()
        {
            if (Game.IsPaused)
            {
                return;
            }

            try
            {
                this.targetDamage = EntityManager9.EnemyHeroes.ToDictionary(x => x, this.GetDamage);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdateKillSteal()
        {
            if (Game.IsPaused)
            {
                return;
            }

            if (this.KillStealMenu.PauseOnCombo && this.targetManager.TargetLocked)
            {
                return;
            }

            try
            {
                foreach (var pair in this.targetDamage)
                {
                    var target = pair.Key;
                    var totalDamage = 0f;

                    if (!target.IsValid || !target.IsVisible || !target.IsAlive || target.IsReflectingDamage || target.IsInvulnerable)
                    {
                        continue;
                    }

                    var abilities = new List<KillStealAbility>();

                    foreach (var abilityPair in pair.Value)
                    {
                        var ability = abilityPair.Key;
                        if (!ability.Ability.IsValid)
                        {
                            continue;
                        }

                        var damage = abilityPair.Value;

                        if (!this.AbilitySleeper.IsSleeping(ability.Ability.Handle) && !ability.Ability.CanBeCasted())
                        {
                            continue;
                        }

                        if (!ability.CanHit(target) || !ability.ShouldCast(target))
                        {
                            continue;
                        }

                        if (ability.Ability.UnitTargetCast && target.IsBlockingAbilities)
                        {
                            continue;
                        }

                        abilities.Add(ability);
                        totalDamage += damage - (ability.Ability.GetHitTime(target) * target.HealthRegeneration * 1.5f);

                        if (totalDamage < target.Health)
                        {
                            continue;
                        }

                        if (this.TargetManager.TargetLocked && !target.Equals(this.TargetManager.Target))
                        {
                            continue;
                        }

                        this.Target = target;
                        this.Kill(target, abilities.Where(x => x.Ability.CanBeCasted()).ToList());
                        return;
                    }
                }

                if (!this.KillStealSleeper.IsSleeping)
                {
                    this.Target = null;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OverlayEnabledOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.damageHandler.IsEnabled = true;
                Drawing.OnDraw += this.OnDraw;
            }
            else
            {
                Drawing.OnDraw -= this.OnDraw;
                if (!this.KillStealMenu.KillStealEnabled)
                {
                    this.damageHandler.IsEnabled = false;
                }
            }
        }

        private void OverlaySizeXOnValueChanged(object sender, SliderEventArgs e)
        {
            this.AdditionalOverlaySize = new Vector2(e.NewValue, this.AdditionalOverlaySize.Y);
        }

        private void OverlaySizeYOnValueChanged(object sender, SliderEventArgs e)
        {
            this.AdditionalOverlaySize = new Vector2(this.AdditionalOverlaySize.X, e.NewValue);
        }

        private void OverlayXOnValueChanged(object sender, SliderEventArgs e)
        {
            this.AdditionalOverlayPosition = new Vector2(e.NewValue, this.AdditionalOverlayPosition.Y);
        }

        private void OverlayYOnValueChanged(object sender, SliderEventArgs e)
        {
            this.AdditionalOverlayPosition = new Vector2(this.AdditionalOverlayPosition.X, e.NewValue);
        }
    }
}