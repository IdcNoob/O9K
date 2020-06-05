namespace O9K.Farm.Damage
{
    using System;

    using Ensage;

    using Units.Base;

    internal class RangedDamage : UnitDamage
    {
        private readonly float hitTime;

        private readonly float includeTime;

        public RangedDamage(UnitDamage damage)
            : base(damage)
        {
            this.hitTime = damage.HitTime + damage.Source.Unit.SecondsPerAttack;
            this.includeTime = damage.IncludeTime + damage.Source.Unit.GetAttackBackswing();
        }

        public RangedDamage(
            FarmUnit source,
            FarmUnit target,
            float attackStartTime,
            float additionalRange,
            float additionalTime,
            float additionalIncludeTime = -0.07f)
            : base(source, target)
        {
            var distance = Math.Max(source.Unit.Distance(target.Unit) - target.Unit.HullRadius - additionalRange, 20)
                           / source.Unit.ProjectileSpeed;
            this.hitTime = attackStartTime + source.Unit.GetAttackPoint(target.Unit) + distance + additionalTime;
            this.includeTime = this.hitTime + additionalIncludeTime;
        }

        public override float HitTime
        {
            get
            {
                if (this.Projectile?.IsValid == true)
                {
                    var distance = Math.Max(this.Target.Unit.Distance(this.Projectile.Position) - (this.Target.Unit.HullRadius * 0.5f), 0);

                    if (this.Target.Unit.IsMoving)
                    {
                        if (this.Target.Unit.GetAngle(this.Projectile.Position) < 1)
                        {
                            distance -= 40;
                        }
                        else
                        {
                            distance += 40;
                        }
                    }

                    return Game.RawGameTime + (distance / this.Source.Unit.ProjectileSpeed);
                }

                if (this.Projectile != null)
                {
                    this.Delete();
                    this.Projectile = null;
                }

                return this.hitTime;
            }
        }

        public override float IncludeTime
        {
            get
            {
                if (this.Projectile?.IsValid == true)
                {
                    var distance = Math.Max(this.Target.Unit.Distance(this.Projectile.Position) - this.Target.Unit.HullRadius, 0);
                    return (Game.RawGameTime + (distance / this.Source.Unit.ProjectileSpeed)) - 0.07f;
                }

                if (this.Projectile != null)
                {
                    this.Delete();
                    this.Projectile = null;
                }

                return this.includeTime;
            }
        }

        public TrackingProjectile Projectile { get; protected set; }

        public void AddProjectile(TrackingProjectile projectile)
        {
            this.Projectile = projectile;
        }
    }
}