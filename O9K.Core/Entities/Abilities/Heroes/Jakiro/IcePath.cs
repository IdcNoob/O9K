namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.jakiro_ice_path)]
    public class IcePath : LineAbility, IDisable
    {
        public IcePath(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "path_radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "path_delay");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}