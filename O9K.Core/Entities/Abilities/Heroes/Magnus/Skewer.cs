namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.magnataur_skewer)]
    public class Skewer : LineAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        public Skewer(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "skewer_radius");
            this.DamageData = new SpecialData(baseAbility, "skewer_damage");
            this.SpeedData = new SpecialData(baseAbility, "skewer_speed");
            this.castRangeData = new SpecialData(baseAbility, "range");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override float CastPoint
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return base.CastPoint / 2f;
                }

                return base.CastPoint;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}