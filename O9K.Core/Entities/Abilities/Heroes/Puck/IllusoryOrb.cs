namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.puck_illusory_orb)]
    public class IllusoryOrb : LineAbility, INuke
    {
        private readonly SpecialData castRangeData;

        public IllusoryOrb(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "orb_speed");
            this.castRangeData = new SpecialData(baseAbility, "max_distance");
        }

        public override float Speed
        {
            get
            {
                return this.SpeedData.GetValueWithTalentMultiply(this.Level);
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValueWithTalentMultiply(this.Level);
            }
        }
    }
}