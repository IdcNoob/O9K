namespace O9K.Evader.Abilities.Heroes.Oracle.FortunesEnd
{
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class FortunesEndUsableDisable : DisableAbility
    {
        private readonly IActionManager actionManager;

        public FortunesEndUsableDisable(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.actionManager = actionManager;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(enemy.Position);
            var use = this.ActiveAbility.UseAbility(enemy, false, true);
            if (!use)
            {
                return false;
            }

            this.actionManager.CancelChanneling(this.ActiveAbility);
            return true;
        }
    }
}