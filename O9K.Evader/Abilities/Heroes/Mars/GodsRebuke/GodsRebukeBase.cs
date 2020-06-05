namespace O9K.Evader.Abilities.Heroes.Mars.GodsRebuke
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mars_gods_rebuke)]
    internal class GodsRebukeBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public GodsRebukeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GodsRebukeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}