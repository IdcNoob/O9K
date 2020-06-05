namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_satanic)]
    public class Satanic : ActiveAbility, IBuff, IHasLifeSteal
    {
        public Satanic(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_item_satanic_unholy";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}