namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_crown)]
    public class Crown : PassiveAbility
    {
        public Crown(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}