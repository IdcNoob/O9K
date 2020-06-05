namespace O9K.Evader.Abilities.Heroes.Slardar.GuardianSprint
{
    using Base;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slardar_sprint)]
    internal class GuardianSprintBase : EvaderBaseAbility, IUsable<DodgeAbility>
    {
        public GuardianSprintBase(Ability9 ability)
            : base(ability)
        {
        }

        public DodgeAbility GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}