namespace O9K.Evader.Abilities.Base.Usable.BlinkAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BlinkLeapAbility : BlinkAbility
    {
        public BlinkLeapAbility(Ability9 ability, IPathfinder pathfinder, IActionManager actionManager, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ActionManager = actionManager;
        }

        protected IActionManager ActionManager { get; }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay(this.FountainPosition) + 0.1f;
        }

        public sealed override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (this.Owner.GetAngle(this.FountainPosition) > 0.4f)
            {
                var position = this.Owner.Position.Extend2D(this.FountainPosition, 30);
                this.Owner.BaseUnit.Move(position, false, true);

                return false;
            }

            this.MoveCamera(ally.Position);
            return this.ActiveAbility.UseAbility(ally, false, true);
        }
    }
}