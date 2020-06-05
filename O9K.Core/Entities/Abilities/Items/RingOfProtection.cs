namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ring_of_protection)]
    public class RingOfProtection : PassiveAbility
    {
        public RingOfProtection(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}