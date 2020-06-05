namespace O9K.Evader.Pathfinder.Obstacles.Abilities.AreaOfEffect
{
    using System;

    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    internal class AreaOfEffectSpeedObstacle : AreaOfEffectObstacle
    {
        private readonly float damageRadius;

        public AreaOfEffectSpeedObstacle(EvadableAbility ability, Vector3 position, float damageRadius, int radiusIncrease = 50)
            : base(ability, position, radiusIncrease)
        {
            this.damageRadius = damageRadius;
            this.Speed = ability.ActiveAbility.Speed;
        }

        protected float Speed { get; }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            var distance = Math.Max(ally.Distance(this.Position) - this.damageRadius, 0);
            return (this.EndCastTime + this.ActivationDelay + (distance / this.Speed)) - Game.RawGameTime;
        }
    }
}