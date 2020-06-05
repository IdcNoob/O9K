namespace O9K.Evader.Abilities.Heroes.Oracle.FalsePromise
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.oracle_false_promise)]
    internal class FalsePromiseBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public FalsePromiseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FalsePromiseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}