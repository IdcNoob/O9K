namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_refresher)]
    [AbilityId(AbilityId.item_refresher_shard)]
    public class RefresherOrb : ActiveAbility
    {
        public RefresherOrb(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}