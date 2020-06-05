namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_take_stash_and_transfer_items)]
    public class TakeStashAndTransferItems : ActiveAbility
    {
        public TakeStashAndTransferItems(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}