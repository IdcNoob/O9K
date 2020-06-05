namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_orb_of_venom)]
    public class OrbOfVenom : PassiveAbility
    {
        public OrbOfVenom(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}