namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_vanguard)]
    public class Vanguard : PassiveAbility
    {
        public Vanguard(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}