namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Projectile
{
    using System;

    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;

    internal class ProjectileObstacle : AbilityObstacle
    {
        public ProjectileObstacle(EvadableAbility ability, TrackingProjectile projectile, Unit9 target)
            : base(ability)
        {
            //todo improve

            this.Projectile = projectile;
            this.Target = target;
            this.Speed = ability.ActiveAbility.Speed;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.Projectile.IsValid;
            }
        }

        protected TrackingProjectile Projectile { get; }

        protected float Speed { get; }

        protected Unit9 Target { get; }

        public override void Draw()
        {
            if (this.Projectile == null)
            {
                return;
            }

            this.Drawer.DrawCircle(this.Projectile.Position, 75);
            this.Drawer.UpdateCirclePosition(this.Projectile.Position);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (this.Projectile.IsValid)
            {
                var range = 75;
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
            return unit.Equals(this.Target);
        }
    }
}