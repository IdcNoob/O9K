namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_talisman_of_evasion)]
    public class TalismanOfEvasion : PassiveAbility
    {
        public TalismanOfEvasion(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}