namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Projectile
{
    using System;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Helpers;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    using Types;

    internal class TargetableProjectileObstacle : AbilityObstacle, IUpdatable
    {
        protected Unit9 Target;

        public TargetableProjectileObstacle(TargetableProjectileEvadable ability)
            : base(ability)
        {
            //todo improve

            const int RangeIncrease = 50;

            this.Radius = 75;
            this.Position = this.Caster.Position;
            this.Speed = ability.ActiveAbility.Speed;
            this.Range = ability.Ability.CastRange + RangeIncrease;
            this.EndCastTime = ability.EndCastTime;
            this.EndObstacleTime = ability.EndCastTime + (ability.ActiveAbility.CastRange / ability.ActiveAbility.Speed);
            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
            this.IsUpdated = false;
        }

        public Vector3 EndPosition { get; protected set; }

        public override bool IsExpired
        {
            get
            {
                if (this.Projectile == null)
                {
                    return base.IsExpired;
                }

                return !this.Projectile.IsValid;
            }
        }

        public bool IsUpdated { get; protected set; }

        public TrackingProjectile Projectile { get; protected set; }

        public float Radius { get; }

        public float Range { get; }

        protected float Speed { get; }

        public void AddProjectile(TrackingProjectile projectile, Unit9 target)
        {
            this.Projectile = projectile;
            this.Target = target;
            this.IsUpdated = true;
        }

        public override void Draw()
        {
            if (this.Projectile == null)
            {
                this.Drawer.DrawRectangle(this.Position, this.EndPosition, this.Radius);
                this.Drawer.UpdateRectanglePosition(this.Position, this.EndPosition, this.Radius);
                return;
            }

            this.Drawer.Dispose(AbilityObstacleDrawer.Type.Rectangle);

            this.Drawer.DrawCircle(this.Projectile.Position, 75);
            this.Drawer.UpdateCirclePosition(this.Projectile.Position);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            if (this.Projectile == null)
            {
                return this.EndCastTime - Game.RawGameTime;
            }

            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (this.Projectile == null)
            {
                var range = 100;
                if (ally.IsMoving)
                {
                    if (ally.GetAngle(this.Position) < 1)
                    {
                        range += 25;
                    }
                    else
                    {
                        range -= 25;
                    }
                }

                var distance = Math.Max(ally.Distance(this.Position) - ((Game.Ping / 1000) * this.Speed) - range, 0);
                return (this.EndCastTime + (distance / this.Speed)) - Game.RawGameTime;
            }

            if (this.Projectile.IsValid)
            {
                var range = 50;
                if (ally.IsMoving)
                {
                    if (ally.GetAngle(this.Projectile.Position) < 1)
                    {
                        range += 25;
                    }
                    else
                    {
                        range -= 25;
                    }
                }

                return Math.Max(ally.Distance(this.Projectile.Position) - ((Game.Ping / 1000) * this.Speed) - range, 0) / this.Speed;
            }

            return 0;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (this.Projectile == null)
            {
                return base.IsIntersecting(unit, checkPrediction);
            }

            return unit.Equals(this.Target);
        }

        public void Update()
        {
            var rectangle = (Polygon.Rectangle)this.Polygon;
            this.EndPosition = this.Caster.InFront(this.Range);
            rectangle.End = this.EndPosition.To2D();
            rectangle.UpdatePolygon();
        }
    }
}