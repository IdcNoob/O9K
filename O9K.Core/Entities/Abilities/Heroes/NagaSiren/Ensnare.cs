namespace O9K.Core.Entities.Abilities.Heroes.NagaSiren
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.naga_siren_ensnare)]
    public class Ensnare : RangedAbility, IDisable, IAppliesImmobility
    {
        public Ensnare(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "net_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_naga_siren_ensnare";
    }
}