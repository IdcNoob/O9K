namespace O9K.Evader.Pathfinder.Obstacles.Abilities.AreaOfEffect
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    internal class AreaOfEffectObstacle : AbilityObstacle
    {
        public AreaOfEffectObstacle(EvadableAbility ability, int radiusIncrease = 50)
            : base(ability)
        {
            this.Radius = ability.ActiveAbility.Radius + radiusIncrease;
        }

        public AreaOfEffectObstacle(EvadableAbility ability, Vector3 position, int radiusIncrease = 50)
            : base(ability)
        {
            this.Position = position;
            this.Radius = ability.ActiveAbility.Radius + radiusIncrease;
            this.Polygon = new Polygon.Circle(this.Position, this.Radius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.Radius);
            this.NavMeshId = 1; // hack
        }

        protected List<uint> NavMeshObstacles { get; set; }

        protected float Radius { get; set; }

        public override void Dispose()
        {
            base.Dispose();
            if (this.NavMeshId != null)
            {
                this.Pathfinder.RemoveNavMeshObstacle(this.NavMeshObstacles);
            }
        }

        public override void Draw()
        {
            this.Drawer.DrawCircle(this.Position, this.Radius);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndObstacleTime - Game.RawGameTime;
        }
    }
}