namespace O9K.Core.Entities.Abilities.Heroes.Snapfire
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.snapfire_mortimer_kisses)]
    public class MortimerKisses : CircleAbility
    {
        private readonly SpecialData maxTravelTimeData;

        private readonly SpecialData minRangeData;

        private readonly SpecialData minTravelTimeData;

        public MortimerKisses(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.RadiusData = new SpecialData(baseAbility, "impact_radius");
            this.minRangeData = new SpecialData(baseAbility, "min_range");
            this.minTravelTimeData = new SpecialData(baseAbility, "min_lob_travel_time");
            this.maxTravelTimeData = new SpecialData(baseAbility, "max_lob_travel_time");
        }

        public float MaxTravelTime
        {
            get
            {
                return this.maxTravelTimeData.GetValue(this.Level);
            }
        }

        public float MinRange
        {
            get
            {
                return this.minRangeData.GetValue(this.Level);
            }
        }

        public float MinTravelTime
        {
            get
            {
                return this.minTravelTimeData.GetValue(this.Level);
            }
        }
    }
}