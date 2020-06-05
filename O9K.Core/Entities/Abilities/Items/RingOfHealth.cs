namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ring_of_health)]
    public class RingOfHealth : PassiveAbility
    {
        public RingOfHealth(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}