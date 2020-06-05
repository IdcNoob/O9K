namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_storm_cyclone)]
    public class Cyclone : RangedAbility, IDisable, IAppliesImmobility
    {
        public Cyclone(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public string ImmobilityModifierName { get; } = "modifier_brewmaster_storm_cyclone";
    }
}