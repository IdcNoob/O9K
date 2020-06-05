namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_take_stash_items)]
    public class RetrieveItems : ActiveAbility
    {
        public RetrieveItems(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}