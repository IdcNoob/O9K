namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ember_spirit_searing_chains)]
    public class SearingChains : RangedAreaOfEffectAbility, IDisable, IAppliesImmobility
    {
        public SearingChains(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_ember_spirit_searing_chains";
    }
}