namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_tome_of_knowledge)]
    public class TomeOfKnowledge : ActiveAbility
    {
        public TomeOfKnowledge(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}