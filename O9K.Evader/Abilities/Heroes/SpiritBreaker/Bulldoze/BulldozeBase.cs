namespace O9K.Evader.Abilities.Heroes.SpiritBreaker.Bulldoze
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spirit_breaker_bulldoze)]
    internal class BulldozeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BulldozeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BulldozeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}