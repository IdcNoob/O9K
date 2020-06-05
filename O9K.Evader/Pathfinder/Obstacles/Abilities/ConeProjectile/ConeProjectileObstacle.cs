namespace O9K.Evader.Pathfinder.Obstacles.Abilities.ConeProjectile
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class ConeProjectileObstacle : AbilityObstacle, IUpdatable
    {
        public ConeProjectileObstacle(ConeProjectileEvadable ability, Vector3 startPosition, int radiusInc, int endRadiusInc, int rangeInc)
            : base(ability)
        {
            this.Position = startPosition;
            this.Speed = ability.ConeAbility.Speed;
            this.Radius = ability.ConeAbility.Radius + radiusInc;
            this.EndRadius = ability.ConeAbility.EndRadius + endRadiusInc;
            this.Range = ability.ConeAbility.Range + rangeInc;
            this.IsUpdated = false;
        }

        public ConeProjectileObstacle(ConeProjectileEvadable ability, Vector3 startPosition)
            : base(ability)
        {
            //todo improve navmesh size ?

            const int RadiusIncrease = 75;
            const int EndRadiusIncrease = 150;
            const int RangeIncrease = 100;

            this.Position = startPosition;
            this.Speed = ability.ConeAbility.Speed;
            this.Radius = ability.ConeAbility.Radius + RadiusIncrease;
            this.EndRadius = ability.ConeAbility.EndRadius + EndRadiusIncrease;
            this.Range = ability.ConeAbility.Range + RangeIncrease;
            this.IsUpdated = false;
        }

        public bool IsUpdated { get; }

        protected Vector3 EndPosition { get; set; }

        protected float EndRadius { get; }

        protected Dictionary<uint, Vector3> NavMeshObstacles { get; set; }

        protected float Radius { get; }

        protected float Range { get; }

        protected float Speed { get; }

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

            var time = Game.RawGameTime - this.EndCastTime;
            if (time < 0)
            {
                return;
            }

            this.Drawer.DrawCircle(this.Position, (this.Radius + this.EndRadius) / 2);
            this.Drawer.UpdateCirclePosition(this.Position.Extend2D(this.EndPosition, time * this.Speed));
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            var distance = Math.Max(ally.Distance(this.Position) - this.GetProjectileRadius(ally.Position), 0);
            return (this.EndCastTime + this.ActivationDelay + (distance / this.Speed)) - Game.RawGameTime;
        }

        public void Update()
        {
            if (this.NavMeshId == null /*&& !this.Caster.IsRotating*/)
            {
                this.EndPosition = this.Caster.InFront(this.Range);
                this.Polygon = new Polygon.Trapezoid(this.Position, this.EndPosition, this.Radius, this.EndRadius);
                this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius, this.EndRadius);
                this.NavMeshId = 1; // hack
            }
            else if (this.NavMeshId != null)
            {
                var time = Game.RawGameTime - this.EndCastTime /*- 0.35f*/;
                if (time < 0)
                {
                    return;
                }

                var currentPosition = this.Position.Extend2D(this.EndPosition, time * this.Speed);

                //foreach (var obstacle in this.NavMeshObstacles)
                //{
                //    var position = this.Position.Extend2D(obstacle.Value, time * this.Speed);
                //    this.Pathfinder.NavMesh.UpdateObstacle(obstacle.Key, position, this.Radius);
                //}

                var rectangle = (Polygon.Trapezoid)this.Polygon;
                rectangle.Start = currentPosition.To2D();
                rectangle.UpdatePolygon();
            }
        }

        protected float GetProjectileRadius(Vector3 position)
        {
            return ((this.EndRadius - this.Radius) * (this.Position.Distance2D(position) / this.Range)) + this.Radius;
        }
    }
}