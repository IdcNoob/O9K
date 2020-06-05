namespace O9K.Evader.Abilities.Heroes.ArcWarden.MagneticField
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Metadata;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal class MagneticFieldUsable : CounterAbility
    {
        private Vector3 castPosition;

        public MagneticFieldUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.castPosition = ally.Position.Extend2D(enemy.Position, -this.ActiveAbility.Radius);
            return this.ActiveAbility.GetHitTime(this.castPosition);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.castPosition);
            return this.ActiveAbility.UseAbility(this.castPosition, false, true);
        }
    }
}