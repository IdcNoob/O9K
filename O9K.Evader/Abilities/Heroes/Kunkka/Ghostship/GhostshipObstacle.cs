namespace O9K.Evader.Abilities.Heroes.Kunkka.Ghostship
{
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Types;

    using SharpDX;

    internal class GhostshipObstacle : AreaOfEffectObstacle, IUpdatable
    {
        public GhostshipObstacle(GhostshipEvadable ability, Unit unit)
            : base(ability)
        {
            this.ObstacleUnit = unit;
            this.Position = unit.Position;
            this.Range = ability.Ghostship.GhostshipDistance;
            this.IsUpdated = false;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.ObstacleUnit.IsValid;
            }
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected Unit ObstacleUnit { get; set; }

        protected float Range { get; }

        public override void Dispose()
        {
            this.Drawer.Dispose();
            if (this.NavMeshId != null)
            {
                this.Pathfinder.RemoveNavMeshObstacle(this.NavMeshObstacles);
            }
        }

        public override void Draw()
        {
            if (this.NavMeshId != null)
            {
                this.Drawer.DrawCircle(this.EndPosition, this.Radius);
            }
        }

        public void Update()
        {
            if (!this.ObstacleUnit.IsVisible)
            {
                return;
            }

            this.EndPosition = this.Position.Extend2D(this.ObstacleUnit.Position, this.Range);
            if (this.Position.Distance(this.EndPosition) < this.Range - 100)
            {
                return;
            }

            this.Polygon = new Polygon.Circle(this.EndPosition, this.Radius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.EndPosition, this.Radius);
            this.NavMeshId = 1; // hack
            this.IsUpdated = true;
        }
    }
}