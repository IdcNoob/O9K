namespace O9K.AutoUsage.Abilities.Special.Unique.DustOfAppearance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Settings;

    using SharpDX;

    [AbilityId(AbilityId.item_dust)]
    internal class DustOfAppearanceAbility : SpecialAbility, IDisposable
    {
        private readonly HashSet<AbilityId> ignoredCastCheckAbilities = new HashSet<AbilityId>
        {
            AbilityId.oracle_false_promise,
            AbilityId.nyx_assassin_vendetta,
            AbilityId.invoker_ghost_walk,
            AbilityId.sandking_sand_storm,
        };

        private readonly HashSet<AbilityId> instantInvisibilities = new HashSet<AbilityId>
        {
            AbilityId.templar_assassin_meld,
        };

        private readonly HashSet<AbilityId> passiveInvisibilities = new HashSet<AbilityId>
        {
            AbilityId.riki_permanent_invisibility,
            AbilityId.treant_natures_guise,
        };

        private readonly Sleeper sleeper = new Sleeper();

        public DustOfAppearanceAbility(IActiveAbility ability, GroupSettings _)
            : base(ability)
        {
        }

        public void Dispose()
        {
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
            Entity.OnParticleEffectReleased -= this.OnParticleEffectReleased;
            EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
            Unit.OnModifierAdded -= this.OnModifierAdded;
        }

        public override void Enabled(bool enabled)
        {
            base.Enabled(enabled);

            if (enabled)
            {
                Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
                Entity.OnParticleEffectReleased += this.OnParticleEffectReleased;
                Unit.OnModifierAdded += this.OnModifierAdded;
                EntityManager9.AbilityMonitor.AbilityCasted += this.OnAbilityCasted;
                EntityManager9.AbilityMonitor.AbilityCastChange += this.OnAbilityCastChange;
            }
            else
            {
                Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
                Entity.OnParticleEffectReleased -= this.OnParticleEffectReleased;
                Unit.OnModifierAdded -= this.OnModifierAdded;
                EntityManager9.AbilityMonitor.AbilityCasted -= this.OnAbilityCasted;
            }
        }

        public bool ForceUse(Unit9 enemy, Vector3 position, float rangeScale = 1f)
        {
            if (this.sleeper || !this.Ability.CanBeCasted())
            {
                return false;
            }

            if (this.Ability.Radius * rangeScale < this.Owner.Distance(position))
            {
                return false;
            }

            if (enemy.HasModifier(ModifierNames.Dust))
            {
                return false;
            }

            if (this.Ability.UseAbility())
            {
                this.sleeper.Sleep(1);
                return true;
            }

            return false;
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.sleeper)
            {
                return false;
            }

            var enemies = heroes.Where(x => x.IsEnemy(this.Owner)).ToList();

            foreach (var enemy in enemies)
            {
                if (enemy.IsInvulnerable || enemy.IsDarkPactProtected)
                {
                    continue;
                }

                if (!this.Ability.CanHit(enemy))
                {
                    continue;
                }

                if (enemy.HasModifier(ModifierNames.Dust))
                {
                    continue;
                }

                if (this.CanBecomeInvisible(enemy) && this.Ability.UseAbility())
                {
                    this.sleeper.Sleep(1);
                    return true;
                }
            }

            return false;
        }

        private bool CanBecomeInvisible(Unit9 unit)
        {
            if (unit.IsInvisible)
            {
                return true;
            }

            foreach (var ability in unit.Abilities)
            {
                if (ability.IsTalent || !ability.IsInvisibility)
                {
                    continue;
                }

                var id = ability.Id;

                //if (this.instantInvisibilities.Contains(id) && ability.CanBeCasted(false))
                //{
                //    return true;
                //}

                if (this.passiveInvisibilities.Contains(id) && ability.RemainingCooldown < 1)
                {
                    return true;
                }
            }

            return false;
        }

        private void ForceUse(Unit9 enemy)
        {
            if (this.sleeper || !this.Ability.CanBeCasted())
            {
                return;
            }

            if (enemy.IsInvulnerable || enemy.IsDarkPactProtected)
            {
                return;
            }

            if (!this.Ability.CanHit(enemy))
            {
                return;
            }

            if (enemy.HasModifier(ModifierNames.Dust))
            {
                return;
            }

            if (this.Ability.UseAbility())
            {
                this.sleeper.Sleep(1);
            }
        }

        private void OnAbilityCastChange(Ability9 ability)
        {
            try
            {
                if (!ability.IsCasting || ability.Id != AbilityId.nyx_assassin_burrow || ability.Owner.IsAlly(this.Owner))
                {
                    return;
                }

                this.ForceUse(ability.Owner);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAbilityCasted(Ability9 ability)
        {
            try
            {
                if (!ability.IsInvisibility || !ability.Owner.IsHero || ability.Owner.IsIllusion || ability.Owner.IsAlly(this.Owner))
                {
                    return;
                }

                if (this.ignoredCastCheckAbilities.Contains(ability.Id))
                {
                    return;
                }

                this.ForceUse(ability.Owner);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                var modifier = args.Modifier;
                if (!modifier.IsDebuff || sender.Team != this.Owner.Team || modifier.Name != "modifier_invoker_ghost_walk_enemy")
                {
                    return;
                }

                var ghostWalk =
                    EntityManager9.Abilities.FirstOrDefault(x => x.Id == AbilityId.invoker_ghost_walk && !x.Owner.IsAlly(this.Owner));
                if (ghostWalk == null)
                {
                    return;
                }

                UpdateManager.BeginInvoke(
                    async () =>
                        {
                            if (!modifier.IsValid)
                            {
                                return;
                            }

                            while (modifier.IsValid)
                            {
                                if (this.ForceUse(ghostWalk.Owner, sender.Position, 0.8f))
                                {
                                    break;
                                }

                                await Task.Delay(300);
                            }
                        });
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            try
            {
                if (args.Name != "particles/units/heroes/hero_sandking/sandking_sandstorm.vpcf")
                {
                    return;
                }

                UpdateManager.BeginInvoke(
                    async () =>
                        {
                            try
                            {
                                var particle = args.ParticleEffect;

                                if (!particle.IsValid)
                                {
                                    return;
                                }

                                var position = particle.GetControlPoint(0);
                                var sandStorm = EntityManager9.Abilities.FirstOrDefault(
                                    x => x.Id == AbilityId.sandking_sand_storm && x.Owner.Distance(position) < 500);

                                if (sandStorm == null || sandStorm.Owner.Team == this.Owner.Team)
                                {
                                    return;
                                }

                                while (particle.IsValid)
                                {
                                    if (this.ForceUse(sandStorm.Owner, position, 0.5f))
                                    {
                                        break;
                                    }

                                    await Task.Delay(300);
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e);
                            }
                        });
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleEffectReleased(Entity sender, ParticleEffectReleasedEventArgs args)
        {
            try
            {
                if (args.ParticleEffect.Name != "particles/units/heroes/hero_nyx_assassin/nyx_assassin_vendetta_start.vpcf")
                {
                    return;
                }

                var particle = args.ParticleEffect;
                var position = particle.GetControlPoint(0);
                var vendetta = EntityManager9.Abilities.FirstOrDefault(
                    x => x.Id == AbilityId.nyx_assassin_vendetta && x.Owner.Distance(position) < 500);

                if (vendetta == null || vendetta.Owner.Team == this.Owner.Team)
                {
                    return;
                }

                this.ForceUse(vendetta.Owner, position);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}