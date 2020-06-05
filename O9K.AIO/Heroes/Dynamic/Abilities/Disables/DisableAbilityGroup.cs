namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Blinks;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Specials;

    internal class DisableAbilityGroup : OldAbilityGroup<IDisable, OldDisableAbility>
    {
        private readonly List<AbilityId> castOrder = new List<AbilityId>
        {
            AbilityId.item_sheepstick
        };

        public DisableAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public BlinkAbilityGroup Blinks { get; set; }

        public SpecialAbilityGroup Specials { get; set; }

        protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
        {
            AbilityId.pudge_meat_hook,
            AbilityId.item_ethereal_blade,
        };

        public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
        {
            foreach (var ability in this.Abilities)
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (except.Contains(ability.Ability.Id))
                {
                    continue;
                }

                if (!ability.CanBeCasted(target, this.Abilities, this.Specials.Abilities, menu))
                {
                    continue;
                }

                if (ability.Use(target))
                {
                    return true;
                }
            }

            return false;
        }

        public bool UseBlinkDisable(Unit9 target, ComboModeMenu menu)
        {
            foreach (var group in this.Abilities.Where(x => x.Ability.IsValid).GroupBy(x => x.Ability.Owner))
            {
                var owner = group.Key;
                var blinkAbilities = this.Blinks.GetBlinkAbilities(owner, menu).ToList();

                if (blinkAbilities.Count == 0)
                {
                    continue;
                }

                var range = blinkAbilities.Sum(x => x.Blink.Range);

                foreach (var ability in group)
                {
                    if (!ability.CanBeCasted(target, menu, false) || !ability.ShouldCast(target))
                    {
                        continue;
                    }

                    if (ability.CanHit(target))
                    {
                        continue;
                    }

                    if (target.IsMagicImmune && !ability.Disable.PiercesMagicImmunity(target))
                    {
                        continue;
                    }

                    if (this.UseTargetable(ability, owner, target, blinkAbilities, range))
                    {
                        return true;
                    }

                    if (this.UseAoe(ability, owner, target, blinkAbilities, range))
                    {
                        // this.OrbwalkSleeper[owner.Handle].ExtendSleep(0.3f);
                        return true;
                    }

                    if (this.UseLine(ability, owner, target, blinkAbilities, range))
                    {
                        // this.OrbwalkSleeper[owner.Handle].ExtendSleep(0.3f);
                        return true;
                    }

                    if (this.UseCircle(ability, owner, target, blinkAbilities, range))
                    {
                        //  this.OrbwalkSleeper[owner.Handle].ExtendSleep(0.3f);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool UseInstantDisable(Unit9 target, ComboModeMenu menu)
        {
            foreach (var ability in this.Abilities)
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (ability.Disable.IsDisarm())
                {
                    continue;
                }

                if (ability.Ability.CastPoint > 0.1f)
                {
                    continue;
                }

                if (!ability.CanBeCasted(target, menu))
                {
                    continue;
                }

                if (ability.Use(target))
                {
                    return true;
                }
            }

            return false;
        }

        public bool UseOnly(Unit9 target, AbilityId abilityId, ComboModeMenu menu)
        {
            var ability = this.Abilities.Find(x => x.Ability.Id == abilityId);
            if (ability == null)
            {
                return false;
            }

            if (!ability.CanBeCasted(target, menu))
            {
                return false;
            }

            return ability.Use(target);
        }

        protected override void OrderAbilities()
        {
            this.Abilities = this.Abilities.OrderBy(x => x.Ability is IChanneled)
                .ThenByDescending(x => this.castOrder.IndexOf(x.Ability.Id))
                .ThenBy(x => x.Ability.CastPoint)
                .ToList();
        }

        private bool UseAoe(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
        {
            if (ability.Disable is AreaOfEffectAbility aoe)
            {
                var input = aoe.GetPredictionInput(target, EntityManager9.EnemyHeroes);
                input.Range += range;
                input.CastRange = range;
                input.SkillShotType = SkillShotType.Circle;

                var output = aoe.GetPredictionOutput(input);
                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                if (this.Blinks.Use(owner, blinkAbilities, output.CastPosition, target))
                {
                    //  UpdateManager.BeginInvoke(() => aoe.UseAbility(), 50);
                    //  ability.SetTimings(target);
                    return true;
                }
            }

            return false;
        }

        private bool UseCircle(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
        {
            if (ability.Disable is CircleAbility circle)
            {
                var input = circle.GetPredictionInput(target, EntityManager9.EnemyHeroes);
                input.CastRange += range;

                var output = circle.GetPredictionOutput(input);
                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                var blinkPosition = owner.IsRanged
                                        ? owner.Position.Extend2D(
                                            output.CastPosition,
                                            Math.Min(range, owner.Position.Distance2D(output.CastPosition) - (owner.GetAttackRange() / 2)))
                                        : output.CastPosition;

                if (this.Blinks.Use(owner, blinkAbilities, blinkPosition, target))
                {
                    // UpdateManager.BeginInvoke(() => circle.UseAbility(output.CastPosition), 50);
                    //  ability.SetTimings(target);
                    return true;
                }
            }

            return false;
        }

        private bool UseLine(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
        {
            if (ability.Disable is LineAbility line)
            {
                var input = line.GetPredictionInput(target, EntityManager9.EnemyHeroes);
                input.CastRange = range;
                input.Range = line.CastRange;
                input.UseBlink = true;

                var output = line.GetPredictionOutput(input);
                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                var blinkPosition = output.BlinkLinePosition;

                if (this.Blinks.Use(owner, blinkAbilities, blinkPosition, target))
                {
                    //  UpdateManager.BeginInvoke(() => line.UseAbility(), 50);
                    //   ability.SetTimings(target);
                    return true;
                }
            }

            return false;
        }

        private bool UseTargetable(OldDisableAbility ability, Unit9 owner, Unit9 target, List<OldBlinkAbility> blinkAbilities, float range)
        {
            if (ability.Disable.UnitTargetCast)
            {
                var blinkPosition = target.Position.Extend2D(owner.Position, ability.Disable.CastRange / 2);

                if (this.Blinks.Use(owner, blinkAbilities, blinkPosition, target))
                {
                    //    ability.Disable.UseAbility(target);
                    //    ability.SetTimings(target);
                    return true;
                }
            }

            return false;
        }
    }
}