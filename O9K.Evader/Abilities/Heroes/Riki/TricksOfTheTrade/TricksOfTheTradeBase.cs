namespace O9K.Evader.Abilities.Heroes.Riki.TricksOfTheTrade
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.riki_tricks_of_the_trade)]
    internal class TricksOfTheTradeBase : EvaderBaseAbility, /* IEvadable,*/ IUsable<CounterAbility>
    {
        public TricksOfTheTradeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            //todo better detection
            return new TricksOfTheTradeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new TricksOfTheTradeUsable(this.Ability, this.ActionManager, this.Menu);
        }
    }
}