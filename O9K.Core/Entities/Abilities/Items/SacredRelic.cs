namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_relic)]
    public class SacredRelic : PassiveAbility
    {
        public SacredRelic(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}