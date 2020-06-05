namespace O9K.Evader.Pathfinder.Obstacles.Abilities.LinearProjectile
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Geometry;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class LinearProjectileObstacle : AbilityObstacle, IUpdatable
    {
        private Sleeper delaySleeper = new Sleeper();

        private bool forceDelay;

        public LinearProjectileObstacle(LinearProjectileEvadable ability, Vector3 startPosition)
            : base(ability)
        {
            const int RadiusIncrease = 50;
            const int RangeIncrease = 100;

            this.Position = startPosition;
            this.Speed = ability.RangedAbility.Speed;
            this.Radius = ability.RangedAbility.Radius + RadiusIncrease;
            this.Range = ability.RangedAbility.Range + RangeIncrease;
            this.IsUpdated = false;
        }

        public LinearProjectileObstacle(LinearProjectileEvadable ability, Vector3 startPosition, Vector3 direction)
            : this(ability, startPosition)
        {
            this.EndPosition = startPosition.Extend2D(direction, this.Range);
            this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
            this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius);
            this.NavMeshId = 1; //hack
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected List<uint> NavMeshObstacles { get; set; }

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

            this.Drawer.DrawArcRectangle(this.Position, this.EndPosition, this.Radius);

            var time = (Game.RawGameTime - this.EndCastTime - this.ActivationDelay) + (this.Radius / 2 / this.Speed);
            if (time < 0)
            {
                return;
            }

            this.Drawer.DrawCircle(this.Position, this.Radius);
            this.Drawer.UpdateCirclePosition(this.Position.Extend2D(this.EndPosition, time * this.Speed));
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public float GetEvadeTime(Vector3 position)
        {
            var distance = Math.Max(position.Distance2D(this.Position) - this.Radius, 0);
            return (this.EndCastTime + this.ActivationDelay + (distance / this.Speed)) - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            var distance = Math.Max(ally.Distance(this.Position) - this.Radius, 0);
            return (this.EndCastTime + this.ActivationDelay + (distance / this.Speed)) - Game.RawGameTime;
        }

        public virtual void Update()
        {
            if (this.NavMeshId == null /*&& !this.Caster.IsRotating*/)
            {
                //if (this.delaySleeper)
                //{
                //    return;
                //}

                //if (!this.forceDelay)
                //{
                //    this.delaySleeper.Sleep(0.1f);
                //    this.forceDelay = true;
                //    return;
                //}

                this.EndPosition = this.Caster.InFront(this.Range);
                this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshObstacles = this.Pathfinder.AddNavMeshObstacle(this.Position, this.EndPosition, this.Radius);
                this.NavMeshId = 1; //hack
            }
            else if (this.NavMeshId != null)
            {
                ////todo check activation delay of all obstacles
                var time = Game.RawGameTime - this.EndCastTime - this.ActivationDelay /*- 0.35f*/;
                if (time < 0)
                {
                    return;
                }

                var currentPosition = this.Position.Extend2D(this.EndPosition, time * this.Speed);

                //this.Pathfinder.NavMesh.UpdateObstacle(this.NavMeshId.Value, currentPosition, this.Radius);
                var rectangle = (Polygon.Rectangle)this.Polygon;
                rectangle.Start = currentPosition.To2D();
                rectangle.UpdatePolygon();
            }
        }
    }
}