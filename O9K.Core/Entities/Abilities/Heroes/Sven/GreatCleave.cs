namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.sven_great_cleave)]
    public class GreatCleave : PassiveAbility
    {
        public GreatCleave(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}