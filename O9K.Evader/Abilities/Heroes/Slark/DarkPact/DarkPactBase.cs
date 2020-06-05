namespace O9K.Evader.Abilities.Heroes.Slark.DarkPact
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slark_dark_pact)]
    internal class DarkPactBase : EvaderBaseAbility, /*IEvadable,*/ IUsable<CounterAbility>
    {
        public DarkPactBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DarkPactEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new DarkPactUsable(this.Ability, this.Menu);
        }
    }
}