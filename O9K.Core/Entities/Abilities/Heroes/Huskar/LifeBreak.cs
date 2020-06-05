namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.huskar_life_break)]
    public class LifeBreak : RangedAbility, INuke
    {
        private readonly SpecialData castRangeBonusData;

        public LifeBreak(Ability baseAbility)
            : base(baseAbility)
        {
            //todo aghanims idisable ?
            this.SpeedData = new SpecialData(baseAbility, "charge_speed");
            this.DamageData = new SpecialData(baseAbility, "health_damage");
            this.castRangeBonusData = new SpecialData(baseAbility.Owner, AbilityId.special_bonus_unique_huskar);
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + this.castRangeBonusData.GetValue(0);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var health = Math.Max(remainingHealth ?? unit.Health, 0);
            var multiplier = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = health * multiplier
            };
        }
    }
}