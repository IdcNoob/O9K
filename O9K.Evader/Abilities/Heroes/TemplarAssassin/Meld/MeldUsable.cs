namespace O9K.Evader.Abilities.Heroes.TemplarAssassin.Meld
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class MeldUsable : CounterInvisibilityAbility
    {
        private readonly IActionManager actionManager;

        public MeldUsable(Ability9 ability, IActionManager actionManager, IMainMenu menu)
            : base(ability, menu)
        {
            this.actionManager = actionManager;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay();
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.Owner.Position);
            if (!this.ActiveAbility.UseAbility(false, true))
            {
                return false;
            }

            this.actionManager.BlockInput(this.Owner, 1);

            return true;
        }
    }
}