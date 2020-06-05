namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_slippers)]
    public class SlippersOfAgility : PassiveAbility
    {
        public SlippersOfAgility(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}