namespace O9K.Evader.Abilities.Heroes.ShadowFiend.Shadowraze
{
    using Base.Evadable;

    using Ensage.SDK.Geometry;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Types;

    internal class ShadowrazeObstacle : AreaOfEffectObstacle, IUpdatable
    {
        private readonly float range;

        public ShadowrazeObstacle(EvadableAbility ability, float range)
            : base(ability)
        {
            this.range = range;
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; private set; }

        public override void Draw()
        {
            if (this.NavMeshId == null)
            {
                return;
            }

            this.Drawer.DrawCircle(this.Position, this.Radius);
        }

        public void Update()
        {
            //if (this.Caster.IsRotating)
            //{
            //    return;
            //}

            this.Position = this.Caster.InFront(this.range);
            this.Polygon = new Polygon.Circle(this.Position, this.Radius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.Radius);
            this.NavMeshId = 1; // hack
            this.IsUpdated = true;
        }
    }
}