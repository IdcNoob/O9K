namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_faded_broach)]
    public class FadedBroach : PassiveAbility
    {
        public FadedBroach(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}