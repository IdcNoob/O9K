namespace O9K.Evader.Abilities.Heroes.Riki.TricksOfTheTrade
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class TricksOfTheTradeUsable : CounterAbility
    {
        private readonly IActionManager actionManager;

        public TricksOfTheTradeUsable(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.actionManager = actionManager;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.Use(ally, enemy, obstacle))
            {
                return false;
            }

            this.actionManager.BlockInput(this.Owner, 1.5f);

            return true;
        }
    }
}