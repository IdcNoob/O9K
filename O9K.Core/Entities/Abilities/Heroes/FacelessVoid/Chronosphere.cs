namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.faceless_void_chronosphere)]
    public class Chronosphere : CircleAbility, IDisable
    {
        public Chronosphere(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}