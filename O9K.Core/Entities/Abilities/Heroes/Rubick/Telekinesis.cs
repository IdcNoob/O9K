namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rubick_telekinesis)]
    public class Telekinesis : RangedAbility, IDisable
    {
        private readonly SpecialData landDistanceData;

        public Telekinesis(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.landDistanceData = new SpecialData(baseAbility, "max_land_distance");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return base.Radius + this.landDistanceData.GetValue(this.Level);
            }
        }
    }
}