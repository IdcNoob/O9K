namespace O9K.Evader.Abilities.Heroes.Huskar.LifeBreak
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.huskar_life_break)]
    internal class LifeBreakBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public LifeBreakBase(Ability9 ability)
            : base(ability)
        {
            //todo usable ?
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LifeBreakEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}