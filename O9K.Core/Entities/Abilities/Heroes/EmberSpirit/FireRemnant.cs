namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ember_spirit_fire_remnant)]
    public class FireRemnant : CircleAbility
    {
        private readonly SpecialData aghanimsRangeMultiplier;

        private readonly SpecialData aghanimsSpeedMultiplier;

        public FireRemnant(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed_multiplier");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.aghanimsSpeedMultiplier = new SpecialData(baseAbility, "scepter_speed_multiplier");
            this.aghanimsRangeMultiplier = new SpecialData(baseAbility, "scepter_range_multiplier");
        }

        public override bool HasAreaOfEffect { get; } = false;

        public override float Speed
        {
            get
            {
                var multiplier = (this.SpeedData.GetValue(this.Level) / 100);

                if (this.Owner.HasAghanimsScepter)
                {
                    multiplier *= this.aghanimsSpeedMultiplier.GetValue(this.Level);
                }

                return this.Owner.Speed * multiplier;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                var castRange = base.BaseCastRange;

                if (this.Owner.HasAghanimsScepter)
                {
                    castRange *= this.aghanimsRangeMultiplier.GetValue(this.Level);
                }

                return castRange;
            }
        }
    }
}