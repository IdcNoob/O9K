namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_necronomicon)]
    [AbilityId(AbilityId.item_necronomicon_2)]
    [AbilityId(AbilityId.item_necronomicon_3)]
    public class Necronomicon : ActiveAbility, IBuff
    {
        public Necronomicon(Ability baseAbility)
            : base(baseAbility)
        {
            this.Name = nameof(AbilityId.item_necronomicon_3);
            this.Id = AbilityId.item_necronomicon_3;
        }

        public string BuffModifierName { get; } = string.Empty;

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}