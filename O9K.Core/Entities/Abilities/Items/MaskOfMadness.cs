namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_mask_of_madness)]
    public class MaskOfMadness : ActiveAbility, IBuff
    {
        public MaskOfMadness(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_item_mask_of_madness_berserk";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}