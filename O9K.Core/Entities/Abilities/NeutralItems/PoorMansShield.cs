namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_poor_mans_shield)]
    public class PoorMansShield : PassiveAbility
    {
        public PoorMansShield(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}