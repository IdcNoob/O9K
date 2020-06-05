namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.earth_spirit_boulder_smash)]
    public class BoulderSmash : RangedAbility, INuke
    {
        private readonly SpecialData castRange;

        public BoulderSmash(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.RangeData = new SpecialData(baseAbility, "rock_distance");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "rock_damage");
            this.castRange = new SpecialData(baseAbility, "rock_search_aoe");
        }

        public override float Range
        {
            get
            {
                return this.RangeData.GetValue(this.Level) + this.Radius;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRange.GetValue(this.Level);
            }
        }
    }
}