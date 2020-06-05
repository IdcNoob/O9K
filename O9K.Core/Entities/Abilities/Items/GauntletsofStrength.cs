namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_gauntlets)]
    public class GauntletsOfStrength : PassiveAbility
    {
        public GauntletsOfStrength(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}