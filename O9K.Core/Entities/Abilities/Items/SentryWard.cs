namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ward_sentry)]
    public class SentryWard : RangedAbility
    {
        public SentryWard(Ability ability)
            : base(ability)
        {
        }
    }
}