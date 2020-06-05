namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.morphling_adaptive_strike_agi)]
    public class AdaptiveStrikeAgility : RangedAbility, INuke
    {
        private readonly SpecialData maxMultiplierData;

        private readonly SpecialData minMultiplierData;

        public AdaptiveStrikeAgility(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.DamageData = new SpecialData(baseAbility, "damage_base");
            this.minMultiplierData = new SpecialData(baseAbility, "damage_min");
            this.maxMultiplierData = new SpecialData(baseAbility, "damage_max");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var baseDamage = base.GetRawDamage(unit, remainingHealth);
            var agility = this.Owner.TotalAgility;
            var strength = this.Owner.TotalStrength;
            var multiplier = agility * 0.75 > strength
                                 ? this.maxMultiplierData.GetValue(this.Level)
                                 : this.minMultiplierData.GetValue(this.Level);
            var agilityDamage = agility * multiplier;

            baseDamage[this.DamageType] += (int)agilityDamage;

            return baseDamage;
        }
    }
}