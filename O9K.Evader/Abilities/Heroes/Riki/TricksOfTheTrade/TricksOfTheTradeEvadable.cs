namespace O9K.Evader.Abilities.Heroes.Riki.TricksOfTheTrade
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TricksOfTheTradeEvadable : AreaOfEffectEvadable
    {
        public TricksOfTheTradeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}