namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_quarterstaff)]
    public class Quarterstaff : PassiveAbility
    {
        public Quarterstaff(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}