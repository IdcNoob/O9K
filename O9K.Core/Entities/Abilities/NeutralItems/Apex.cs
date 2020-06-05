namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_apex)]
    public class Apex : PassiveAbility
    {
        public Apex(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}