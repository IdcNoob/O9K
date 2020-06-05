namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_shadow_amulet)]
    public class ShadowAmulet : RangedAbility, IShield
    {
        public ShadowAmulet(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public override bool IsInvisibility { get; } = true;

        public string ShieldModifierName { get; } = "modifier_item_shadow_amulet_fade";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}