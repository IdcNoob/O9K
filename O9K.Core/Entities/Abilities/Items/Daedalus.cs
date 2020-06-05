namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_greater_crit)]
    public class Daedalus : PassiveAbility
    {
        public Daedalus(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}