namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.legion_commander_moment_of_courage)]
    public class MomentOfCourage : PassiveAbility
    {
        public MomentOfCourage(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}