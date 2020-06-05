namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.Illuminate
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class IlluminateUsableCounter : CounterEnemyAbility
    {
        private readonly IActionManager actionManager;

        public IlluminateUsableCounter(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.actionManager = actionManager;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(enemy.Position);
            var use = this.ActiveAbility.UseAbility(enemy, HitChance.Medium, false, true);
            if (!use)
            {
                return false;
            }

            this.actionManager.CancelChanneling(this.ActiveAbility);
            return true;
        }
    }
}