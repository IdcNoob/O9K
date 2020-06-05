namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_broadsword)]
    public class Broadsword : PassiveAbility
    {
        public Broadsword(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}