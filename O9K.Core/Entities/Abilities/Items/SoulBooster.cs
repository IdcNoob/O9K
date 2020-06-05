namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_soul_booster)]
    public class SoulBooster : PassiveAbility
    {
        public SoulBooster(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}