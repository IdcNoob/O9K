namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_hyperstone)]
    public class Hyperstone : PassiveAbility
    {
        public Hyperstone(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}