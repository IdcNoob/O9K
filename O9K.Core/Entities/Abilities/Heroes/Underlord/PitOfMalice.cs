namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.abyssal_underlord_pit_of_malice)]
    public class PitOfMalice : CircleAbility, IDisable, IAppliesImmobility
    {
        public PitOfMalice(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_abyssal_underlord_pit_of_malice_ensare";
    }
}