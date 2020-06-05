namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.medusa_split_shot)]
    public class SplitShot : PassiveAbility
    {
        public SplitShot(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}