namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_heavens_halberd)]
    public class HeavensHalberd : RangedAbility, IDisable
    {
        public HeavensHalberd(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Disarmed;
    }
}