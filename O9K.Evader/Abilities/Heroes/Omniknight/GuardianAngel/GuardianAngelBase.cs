namespace O9K.Evader.Abilities.Heroes.Omniknight.GuardianAngel
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.omniknight_guardian_angel)]
    internal class GuardianAngelBase : EvaderBaseAbility, IEvadable //, IUsable<CounterAbility>
    {
        public GuardianAngelBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GuardianAngelEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}