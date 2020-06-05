namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Global
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;

    internal class GlobalObstacle : AbilityObstacle
    {
        public GlobalObstacle(EvadableAbility ability)
            : base(ability)
        {
        }

        public override void Draw()
        {
        }

        public override float GetDisableTime(Unit9 enemy)
        {
            return this.EndCastTime - Game.RawGameTime;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            return this.EndObstacleTime - Game.RawGameTime;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            return true;
        }
    }
}