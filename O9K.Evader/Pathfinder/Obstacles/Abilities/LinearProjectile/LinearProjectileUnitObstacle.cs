namespace O9K.Evader.Pathfinder.Obstacles.Abilities.LinearProjectile
{
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    internal class LinearProjectileUnitObstacle : LinearProjectileObstacle
    {
        public LinearProjectileUnitObstacle(LinearProjectileEvadable ability, Vector3 startPosition)
            : base(ability, startPosition)
        {
        }

        public LinearProjectileUnitObstacle(LinearProjectileEvadable ability, Unit unit)
            : base(ability, unit.Position)
        {
            this.ObstacleUnit = unit;
        }

        public override bool IsExpired
        {
            get
            {
                if (this.ObstacleUnit != null)
                {
                    return !this.ObstacleUnit.IsValid;
                }

                return base.IsExpired;
            }
        }

        protected Unit ObstacleUnit { get; set; }

        public virtual void AddUnit(Unit unit)
        {
            this.ObstacleUnit = unit;
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            if (this.ObstacleUnit == null)
            {
                return base.GetDisableTime(enemy);
            }

            return 0;
        }

        public override void Update()
        {
            if (this.NavMeshId == null && this.ObstacleUnit != null)
            {
                if (!this.ObstacleUnit.IsVisible)
                {
                    return;
                }

                this.EndPosition = this.Position.Extend2D(this.ObstacleUnit.Position, this.Range);
                this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshId = 1; //hack
                return;
            }

            base.Update();
        }
    }
}