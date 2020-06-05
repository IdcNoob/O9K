namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_clumsy_net)]
    public class ClumsyNet : RangedAbility, IDisable, IAppliesImmobility
    {
        public ClumsyNet(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_clumsy_net_ensnare";

        public override float Speed { get; } = 900;
    }
}