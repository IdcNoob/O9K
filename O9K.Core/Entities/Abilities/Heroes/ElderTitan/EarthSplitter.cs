namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.elder_titan_earth_splitter)]
    public class EarthSplitter : LineAbility
    {
        public EarthSplitter(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "crack_time");
            this.RadiusData = new SpecialData(baseAbility, "crack_width");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage_pct");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = unit.MaximumHealth * (this.DamageData.GetValue(this.Level) / 100) * 0.5f;

            return new Damage
            {
                [DamageType.Magical] = damage,
                [DamageType.Physical] = damage
            };
        }
    }
}