namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_gloves)]
    public class GlovesOfHaste : PassiveAbility
    {
        public GlovesOfHaste(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}