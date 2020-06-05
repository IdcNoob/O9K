namespace O9K.AutoUsage.Abilities.Special.Unique.SpellSteal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Settings;

    [AbilityId(AbilityId.rubick_spell_steal)]
    internal class SpellStealAbility : SpecialAbility, IDisposable
    {
        private readonly Dictionary<uint, float> abilityCastTimes = new Dictionary<uint, float>();

        private readonly Dictionary<Unit9, ActiveAbility> casted = new Dictionary<Unit9, ActiveAbility>();

        private readonly HashSet<AbilityId> checkByPhase = new HashSet<AbilityId>
        {
            AbilityId.bloodseeker_rupture,
            AbilityId.morphling_waveform,
            AbilityId.death_prophet_spirit_siphon,
            AbilityId.shadow_demon_demonic_purge,
        };

        private readonly IUpdateHandler handler;

        private readonly HashSet<AbilityId> ignoredAbilities = new HashSet<AbilityId>
        {
            AbilityId.rubick_spell_steal,
            AbilityId.morphling_morph_replicate,
            AbilityId.morphling_replicate,
            AbilityId.skeleton_king_mortal_strike,
            AbilityId.invoker_quas,
            AbilityId.invoker_wex,
            AbilityId.invoker_exort,
            AbilityId.invoker_invoke,
            AbilityId.life_stealer_assimilate,
            AbilityId.skeleton_king_vampiric_aura,
            AbilityId.tusk_walrus_kick,
            AbilityId.kunkka_tidebringer,
            AbilityId.kunkka_return,
            AbilityId.enchantress_impetus,
            AbilityId.doom_bringer_infernal_blade,
            AbilityId.jakiro_liquid_fire,
            AbilityId.lone_druid_true_form_druid,
            AbilityId.puck_ethereal_jaunt,
            AbilityId.spectre_reality,
            AbilityId.phoenix_icarus_dive_stop,
            AbilityId.phoenix_launch_fire_spirit,
            AbilityId.phoenix_sun_ray_stop,
            AbilityId.phoenix_sun_ray_toggle_move,
            AbilityId.tusk_launch_snowball,
        };

        private readonly SpellStealSettings settings;

        private readonly MenuSwitcher useInvisible;

        public SpellStealAbility(IActiveAbility ability, GroupSettings settings)
            : base(ability)
        {
            this.settings = new SpellStealSettings(settings.Menu, ability);
            this.handler = UpdateManager.Subscribe(this.OnUpdate, 500, false);
            EntityManager9.AbilityAdded += this.OnAbilityAdded;
            this.useInvisible = settings.UseWhenInvisible;
        }

        public void Dispose()
        {
            UpdateManager.Unsubscribe(this.handler);
            EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
            EntityManager9.AbilityMonitor.AbilityCastChange -= this.OnAbilityCastChange;
            EntityManager9.AbilityAdded -= this.OnAbilityAdded;
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                this.handler.IsEnabled = true;
                EntityManager9.AbilityMonitor.AbilityCasted += this.OnAbilityCasted;
                EntityManager9.AbilityMonitor.AbilityCastChange += this.OnAbilityCastChange;
            }
            else
            {
                this.handler.IsEnabled = false;
                EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
                EntityManager9.AbilityMonitor.AbilityCastChange -= this.OnAbilityCastChange;
            }
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (!this.Ability.CanBeCasted())
            {
                return false;
            }

            var stolen = this.Owner.Abilities.Where(x => x.IsStolen && x.AbilitySlot >= 0).ToList();

            foreach (var pair in this.casted.ToList())
            {
                var castedAbility = pair.Value;

                if (stolen.Any(x => x.Name == castedAbility.Name))
                {
                    continue;
                }

                if (!this.settings.IsHighPriorityAbilityEnabled(castedAbility))
                {
                    if (!this.settings.IsAbilityEnabled(castedAbility)
                        && (!this.settings.IsScepterAbilityEnabled(castedAbility) || !this.Owner.HasAghanimsScepter))
                    {
                        continue;
                    }

                    if (stolen.Count > 0 && stolen.All(x => x.CanBeCasted()))
                    {
                        continue;
                    }
                }

                var caster = pair.Key;
                if (caster.IsInvulnerable || !caster.IsVisible || this.Owner.Distance(caster) > this.Ability.CastRange)
                {
                    continue;
                }

                if (this.Ability.UseAbility(caster))
                {
                    this.casted.Remove(caster);
                    return true;
                }

                return false;
            }

            return false;
        }

        private void OnAbilityAdded(Ability9 ability)
        {
            try
            {
                if (ability.IsItem || ability.Owner.IsAlly(this.Owner) || !ability.Owner.IsHero || ability.Owner.IsIllusion)
                {
                    return;
                }

                if (this.ignoredAbilities.Contains(ability.Id) || !(ability is ActiveAbility))
                {
                    return;
                }

                this.settings.Add(ability);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityCastChange(Ability9 ability)
        {
            try
            {
                if (!this.checkByPhase.Contains(ability.Id))
                {
                    return;
                }

                if (!(ability is ActiveAbility active))
                {
                    return;
                }

                if (ability.IsCasting)
                {
                    this.abilityCastTimes[ability.Handle] = Game.RawGameTime;
                }
                else if (this.abilityCastTimes.TryGetValue(ability.Handle, out var castStartTime))
                {
                    if (castStartTime + active.CastPoint <= Game.RawGameTime)
                    {
                        this.OnAbilityCasted(ability);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityCasted(Ability9 ability9)
        {
            try
            {
                var ability = ability9 as ActiveAbility;
                if (ability == null || ability.IsItem || this.ignoredAbilities.Contains(ability.Id))
                {
                    return;
                }

                var caster = ability.Owner;
                if (!caster.IsHero || caster.IsIllusion || caster.IsAlly(this.Owner))
                {
                    return;
                }

                this.casted[caster] = ability;

                if (!this.useInvisible && !this.Owner.CanUseAbilitiesInInvisibility && this.Owner.IsInvisible)
                {
                    return;
                }

                this.UseAbility(null);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                foreach (var pair in this.casted.ToList())
                {
                    var hero = pair.Key;

                    if (!hero.IsValid || !hero.IsVisible || !hero.IsAlive)
                    {
                        this.casted.Remove(hero);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}