namespace O9K.Evader.Abilities.Heroes.Mars.Bulwark
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal class BulwarkUsable : CounterAbility
    {
        private Vector3 movePosition;

        public BulwarkUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (!ally.Equals(this.Owner))
            {
                return false;
            }

            if (!this.Ability.CanBeCasted())
            {
                return false;
            }

            var angle = ally.GetAngle(enemy.Position);
            if (angle < 1)
            {
                return false;
            }

            if (this.Owner.IsStunned || this.Owner.IsChanneling)
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.movePosition = this.Owner.Position.Extend2D(enemy.Position, 25);
            return this.Owner.GetTurnTime(this.movePosition) + (Game.Ping / 2000f) + 0.05f;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.movePosition);
            return this.Owner.BaseUnit.Move(this.movePosition, false, true);
        }
    }
}