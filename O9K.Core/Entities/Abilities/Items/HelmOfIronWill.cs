namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_helm_of_iron_will)]
    public class HelmOfIronWill : PassiveAbility
    {
        public HelmOfIronWill(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}