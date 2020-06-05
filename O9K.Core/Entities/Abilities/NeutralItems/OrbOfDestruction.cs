namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_orb_of_destruction)]
    public class OrbOfDestruction : PassiveAbility
    {
        public OrbOfDestruction(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}