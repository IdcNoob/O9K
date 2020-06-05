namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_gem)]
    public class GemOfTrueSight : PassiveAbility
    {
        public GemOfTrueSight(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}