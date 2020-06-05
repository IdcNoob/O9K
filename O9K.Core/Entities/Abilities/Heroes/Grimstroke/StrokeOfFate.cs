namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.grimstroke_dark_artistry)]
    public class StrokeOfFate : LineAbility, INuke, IDebuff
    {
        private readonly SpecialData castRangeData;

        public StrokeOfFate(Ability baseAbility)
            : base(baseAbility)
        {
            //todo better damage calc

            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.castRangeData = new SpecialData(baseAbility, "abilitycastrange");
        }

        public string DebuffModifierName { get; } = "modifier_grimstroke_dark_artistry_slow";

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValueWithTalentMultiply(this.Level)
            };
        }
    }
}