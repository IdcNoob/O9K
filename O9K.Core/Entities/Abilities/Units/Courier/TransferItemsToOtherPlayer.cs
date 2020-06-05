namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_transfer_items_to_other_player)]
    public class TransferItemsToOtherPlayer : RangedAbility
    {
        public TransferItemsToOtherPlayer(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}