namespace O9K.Evader.Abilities.Heroes.Disruptor.Glimpse
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities;

    internal class GlimpseObstacle : AbilityObstacle
    {
        private readonly Unit9 glimpseTarget;

        public GlimpseObstacle(EvadableAbility ability, Unit9 target)
            : base(ability)
        {
            this.glimpseTarget = target;
        }

        public override void Draw()
        {
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndObstacleTime - Game.RawGameTime;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            return unit.Equals(this.glimpseTarget);
        }
    }
}