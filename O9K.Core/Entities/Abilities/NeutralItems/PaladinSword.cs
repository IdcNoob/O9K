namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_paladin_sword)]
    public class PaladinSword : PassiveAbility
    {
        public PaladinSword(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}