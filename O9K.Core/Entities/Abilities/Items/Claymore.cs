namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_claymore)]
    public class Claymore : PassiveAbility
    {
        public Claymore(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}