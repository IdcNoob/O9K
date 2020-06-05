namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ultimate_orb)]
    public class UltimateOrb : PassiveAbility
    {
        public UltimateOrb(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}