namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_craggy_coat)]
    public class CraggyCoat : PassiveAbility
    {
        public CraggyCoat(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}