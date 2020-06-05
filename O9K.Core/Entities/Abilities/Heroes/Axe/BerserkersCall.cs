namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.axe_berserkers_call)]
    public class BerserkersCall : AreaOfEffectAbility, IDisable, IAppliesImmobility
    {
        public BerserkersCall(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ImmobilityModifierName { get; } = "modifier_axe_berserkers_call";
    }
}