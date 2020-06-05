namespace O9K.AIO.Heroes.Dynamic.Abilities.Blinks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Extensions;

    using Disables;

    using Ensage;

    using SharpDX;

    using Specials;

    internal class BlinkAbilityGroup : OldAbilityGroup<IBlink, OldBlinkAbility>
    {
        private readonly List<AbilityId> castOrder = new List<AbilityId>
        {
            AbilityId.item_force_staff,
            AbilityId.item_hurricane_pike,
            AbilityId.item_blink
        };

        private readonly HashSet<AbilityId> forceBlinkAbilities = new HashSet<AbilityId>
        {
            AbilityId.legion_commander_duel,
            AbilityId.batrider_flaming_lasso
        };

        public BlinkAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public DisableAbilityGroup Disables { get; set; }

        public SpecialAbilityGroup Specials { get; set; }

        protected override HashSet<AbilityId> Ignored { get; } = new HashSet<AbilityId>
        {
            AbilityId.ember_spirit_activate_fire_remnant,
            AbilityId.item_hurricane_pike
        };

        public IEnumerable<OldBlinkAbility> GetBlinkAbilities(Unit9 owner, ComboModeMenu menu)
        {
            return this.Abilities.Where(x => x.Ability.IsValid && x.Ability.Owner.Equals(owner) && x.CanBeCasted(menu));
        }

        public bool Use(Unit9 owner, List<OldBlinkAbility> blinkAbilities, Vector3 position, Unit9 target)
        {
            var distance = owner.Distance(position);

            switch (this.UseSingleBlink(owner, blinkAbilities, position, distance))
            {
                case null:
                    return false;
                case true:
                    return true;
                case false:
                    return this.UseComboBlink(owner, blinkAbilities, distance, position, target);
            }

            return false;
        }

        public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
        {
            foreach (var group in this.Abilities.Where(x => x.Ability.IsValid).GroupBy(x => x.Ability.Owner))
            {
                var owner = group.Key;

                foreach (var blinkAbility in group)
                {
                    if (except.Contains(blinkAbility.Ability.Id))
                    {
                        continue;
                    }

                    if (!blinkAbility.CanBeCasted(menu))
                    {
                        continue;
                    }

                    var blinkPosition = this.GetBlinkPosition(owner, target, menu);
                    if (blinkPosition.IsZero)
                    {
                        continue;
                    }

                    if (owner.Distance(blinkPosition) > blinkAbility.Blink.Range)
                    {
                        continue;
                    }

                    if (blinkAbility.Blink.PositionCast && blinkAbility.Use(blinkPosition))
                    {
                        return true;
                    }

                    if (owner.GetAngle(blinkPosition) < 0.2 && blinkAbility.Use(owner))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Use(Hero9 target, ComboModeMenu menu, float useRange, float blinkRange)
        {
            foreach (var group in this.Abilities.Where(x => x.Ability.IsValid).GroupBy(x => x.Ability.Owner))
            {
                var owner = group.Key;
                var distance = owner.Distance(target);

                if (distance < useRange)
                {
                    continue;
                }

                foreach (var blinkAbility in group)
                {
                    if (!blinkAbility.CanBeCasted(menu))
                    {
                        continue;
                    }

                    var position = target.Position.Extend2D(owner.Position, blinkRange);

                    if (owner.Distance(position) > blinkAbility.Blink.Range)
                    {
                        continue;
                    }

                    if (blinkAbility.Blink.PositionCast && blinkAbility.Use(position))
                    {
                        return true;
                    }

                    if (owner.GetAngle(position) < 0.2 && blinkAbility.Use(owner))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override bool IsIgnored(Ability9 ability)
        {
            if (base.IsIgnored(ability))
            {
                return true;
            }

            return ability is IDisable || ability is INuke;
        }

        protected override void OrderAbilities()
        {
            this.Abilities = this.Abilities.OrderByDescending(x => this.castOrder.IndexOf(x.Ability.Id))
                .ThenBy(x => x.Ability.CastPoint)
                .ToList();
        }

        private Vector3 GetBlinkPosition(Unit9 owner, Unit9 target, ComboModeMenu menu)
        {
            if (this.Disables.CanBeCasted(this.forceBlinkAbilities, menu, target))
            {
                if (target.IsMoving)
                {
                    return target.Position;
                }
                else
                {
                    return target.Position.Extend2D(owner.Position, 25);
                }
            }

            if (this.Disables.CanBeCasted(
                    new HashSet<AbilityId>
                    {
                        AbilityId.item_cyclone
                    },
                    menu,
                    target) && this.Specials.CanBeCasted(
                    new HashSet<AbilityId>
                    {
                        AbilityId.item_cyclone
                    },
                    menu,
                    target))
            {
                return target.Position.Extend2D(owner.Position, 25);
            }

            var position = owner.IsRanged ? target.Position.Extend2D(owner.Position, owner.GetAttackRange() * 0.75f) : target.InFront(25);
            if (owner.Distance(target) < owner.GetAttackRange(target, 100))
            {
                return Vector3.Zero;
            }

            return position;
        }

        private bool UseComboBlink(Unit9 owner, IEnumerable<OldBlinkAbility> blinkAbilities, float distance, Vector3 position, Unit9 target)
        {
            var use = false;
            var comboBlink = new List<OldBlinkAbility>();

            foreach (var blinkAbility in blinkAbilities)
            {
                distance -= blinkAbility.Blink.Range;
                comboBlink.Add(blinkAbility);

                if (distance < 0)
                {
                    use = true;
                    break;
                }
            }

            if (use)
            {
                var blinkAbility = comboBlink.LastOrDefault();
                if (blinkAbility == null)
                {
                    return false;
                }

                var range = Math.Min(owner.Distance(position), blinkAbility.Blink.Range);
                var blinkPosition = owner.Position.Extend2D(position, range);

                if (this.UseSingleBlink(owner, new[] { blinkAbility }, blinkPosition, distance) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private bool? UseSingleBlink(Unit9 owner, IEnumerable<OldBlinkAbility> blinkAbilities, Vector3 position, float distance)
        {
            foreach (var blinkAbility in blinkAbilities)
            {
                if (blinkAbility.Blink.Range < distance)
                {
                    continue;
                }

                switch (blinkAbility.Blink.BlinkType)
                {
                    case BlinkType.Blink:
                    {
                        if (!blinkAbility.Use(position))
                        {
                            return false;
                        }

                        if (blinkAbility.Ability.Speed <= 0)
                        {
                            return true;
                        }

                        return null;
                    }
                    case BlinkType.Leap:
                    {
                        if (owner.GetAngle(position) > 0.2)
                        {
                            owner.BaseUnit.Move(owner.Position.Extend2D(position, 25));
                            this.OrbwalkSleeper.Sleep(owner.Handle, owner.GetTurnTime(position));
                            return null;
                        }

                        if (!blinkAbility.Use(owner))
                        {
                            return false;
                        }

                        if (blinkAbility.Ability.Speed <= 0)
                        {
                            return true;
                        }

                        return null;
                    }
                }
            }

            return false;
        }
    }
}