namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_demonicon)]
    public class BookOfTheDead : ActiveAbility, IBuff
    {
        public BookOfTheDead(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = string.Empty;

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}