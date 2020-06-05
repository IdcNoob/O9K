namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Cone
{
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class ConeObstacle : AbilityObstacle, IUpdatable
    {
        public ConeObstacle(ConeEvadable ability, Vector3 startPosition)
            : base(ability)
        {
            //todo improve navmesh size ?

            const int RadiusIncrease = 75;
            const int EndRadiusIncrease = 150;
            const int RangeIncrease = 75;

            this.Position = startPosition;
            this.Radius = ability.ConeAbility.Radius + RadiusIncrease;
            this.EndRadius = ability.ConeAbility.EndRadius + EndRadiusIncrease;
            this.Range = ability.ConeAbility.Range + RangeIncrease;
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected float EndRadius { get; }

        protected Dictionary<uint, Vector3> NavMeshObstacles { get; set; }

        protected float Radius { get; }

        protected float Range { get; }

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
            if (this.NavMeshId == null)
            {
                return;
            }

            this.Drawer.DrawArcRectangle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndObstacleTime - Game.RawGameTime;
        }

        public void Update()
        {
            //if (this.Caster.IsRotating)
            //{
            //    return;
            //}

            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Trapezoid(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
            this.NavMeshId = 1; // hack

            this.IsUpdated = true;
        }
    }
}