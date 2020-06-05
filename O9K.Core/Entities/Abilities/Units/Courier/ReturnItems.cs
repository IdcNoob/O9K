namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_return_stash_items)]
    public class ReturnItems : ActiveAbility
    {
        public ReturnItems(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}