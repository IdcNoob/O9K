namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Projectile;

    internal abstract class ProjectileEvadable : TargetableProjectileEvadable
    {
        protected ProjectileEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool CanBeDodged { get; } = false;

        public override void AddProjectile(TrackingProjectile projectile, Unit9 target)
        {
            var projectileObstacle = new ProjectileObstacle(this, projectile, target);
            this.Pathfinder.AddObstacle(projectileObstacle);
        }

        public sealed override void PhaseCancel()
        {
        }

        public sealed override void PhaseStart()
        {
        }

        protected sealed override void AddObstacle()
        {
        }
    }
}