namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_witless_shako)]
    public class WitlessShako : PassiveAbility
    {
        public WitlessShako(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}