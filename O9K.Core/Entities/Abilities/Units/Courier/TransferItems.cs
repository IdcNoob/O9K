namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_transfer_items)]
    public class TransferItems : ActiveAbility
    {
        public TransferItems(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}