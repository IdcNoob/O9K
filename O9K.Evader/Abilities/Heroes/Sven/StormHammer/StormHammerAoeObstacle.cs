namespace O9K.Evader.Abilities.Heroes.Sven.StormHammer
{
    using System;

    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Helpers;

    using Pathfinder.Obstacles.Abilities;
    using Pathfinder.Obstacles.Types;

    using SharpDX;

    internal class StormHammerAoeObstacle : AbilityObstacle, IUpdatable
    {
        protected TrackingProjectile Projectile;

        protected Unit9 Target;

        public StormHammerAoeObstacle(StormHammerEvadable ability)
            : base(ability)
        {
            const int RadiusIncrease = 50;
            const int RangeIncrease = 100;

            this.Position = this.Caster.Position;
            this.Speed = ability.RangedAbility.Speed;
            this.Radius = ability.RangedAbility.Radius + RadiusIncrease;
            this.Range = ability.RangedAbility.Range + RangeIncrease;
            this.EndPosition = this.Caster.InFront(this.Range);
            this.Polygon = new Polygon.Rectangle(this.Position, this.EndPosition, this.Radius);
            this.IsUpdated = false;
        }

        public StormHammerAoeObstacle(StormHammerEvadable ability, TrackingProjectile projectile, Unit9 target)
            : this(ability)
        {
            const int RadiusIncrease = 50;
            const int RangeIncrease = 100;

            this.Speed = ability.RangedAbility.Speed;
            this.Radius = ability.RangedAbility.Radius + RadiusIncrease;
            this.Range = ability.RangedAbility.Range + RangeIncrease;
            this.EndObstacleTime = Game.RawGameTime + (this.Range / this.Speed);

            this.AddProjectile(projectile, target);
        }

        public override bool IsExpired
        {
            get
            {
                if (this.Projectile?.IsValid == false)
                {
                    return true;
                }

                return base.IsExpired;
            }
        }

        public bool IsUpdated { get; protected set; }

        protected Vector3 EndPosition { get; set; }

        protected float Radius { get; }

        protected float Range { get; }

        protected float Speed { get; }

        public void AddProjectile(TrackingProjectile projectile, Unit9 target)
        {
            this.Drawer.Dispose(AbilityObstacleDrawer.Type.Rectangle);
            this.Projectile = projectile;
            this.Target = target;
            this.IsUpdated = true;
        }

        public override void Draw()
        {
            if (this.Projectile != null)
            {
                this.Drawer.DrawCircle(this.Target.Position, this.Radius);
                this.Drawer.UpdateCirclePosition(this.Target.Position);
                return;
            }

            this.Drawer.DrawArcRectangle(this.Position, this.EndPosition, this.Radius);
            this.Drawer.UpdateRectanglePosition(this.Position, this.EndPosition, this.Radius);

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
                if (this.Target.IsMoving)
                {
                    if (this.Target.GetAngle(this.Projectile.Position) < 1)
                    {
                        range += 25;
                    }
                    else
                    {
                        range -= 25;
                    }
                }

                return Math.Max(this.Target.Distance(this.Projectile.Position) - ((Game.Ping / 1000) * this.Speed) - range, 0) / this.Speed;
            }

            return 0;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (this.Projectile != null)
            {
                return unit.Distance(this.Target) < this.Radius + 50;
            }

            return base.IsIntersecting(unit, checkPrediction);
        }

        public virtual void Update()
        {
            var rectangle = (Polygon.Rectangle)this.Polygon;
            this.EndPosition = this.Caster.InFront(this.Range);
            rectangle.End = this.EndPosition.To2D();
            rectangle.UpdatePolygon();
        }
    }
}