namespace O9K.Evader.Abilities.Heroes.Gyrocopter.HomingMissile
{
    using System;
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Abilities.Heroes.Gyrocopter;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Pathfinder.Obstacles.Abilities;

    using SharpDX;

    internal class HomingMissileObstacle : AbilityObstacle
    {
        private readonly HomingMissile homingMissileAbility;

        private readonly Vector3 initialPosition;

        private readonly float initialTime;

        private readonly Unit missileUnit;

        public HomingMissileObstacle(EvadableAbility ability, Unit missileUnit)
            : base(ability)
        {
            this.homingMissileAbility = (HomingMissile)ability.Ability;
            this.missileUnit = missileUnit;
            this.initialPosition = missileUnit.Position;
            this.initialTime = Game.RawGameTime + this.homingMissileAbility.ActivationDelay;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.missileUnit.IsValid || !this.missileUnit.IsAlive;
            }
        }

        private float Speed
        {
            get
            {
                return this.homingMissileAbility.Speed + (this.homingMissileAbility.Acceleration * (Game.RawGameTime - this.initialTime));
            }
        }

        public override void Draw()
        {
            this.Drawer.DrawCircle(this.missileUnit.Position, 75);
            this.Drawer.UpdateCirclePosition(this.missileUnit.Position);
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return Math.Max(ally.Distance(this.missileUnit.NetworkPosition) - (50 + Game.Ping), 0.1f) / this.Speed;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            var position = this.missileUnit.NetworkPosition;
            if (this.initialPosition == position)
            {
                return false;
            }

            var missile = EntityManager9.GetUnit(this.missileUnit.Handle);
            if (missile == null)
            {
                return false;
            }

            var target = EntityManager9.Units.Where(x => x.IsEnemy(this.Caster) && x.IsAlive && x.IsUnit)
                .OrderBy(x => missile.GetAngle(x.Position))
                .FirstOrDefault();

            if (target == null)
            {
                return false;
            }

            return unit.Equals(target);
        }
    }
}