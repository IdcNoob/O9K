namespace O9K.Evader.Abilities.Heroes.Lion.FingerOfDeath
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.Targetable;

    internal class FingerOfDeathObstacle : TargetableObstacle
    {
        public FingerOfDeathObstacle(EvadableAbility ability, float radius = 75)
            : base(ability, radius)
        {
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (blink)
            {
                return this.GetDisableTime(null);
            }

            return this.EndObstacleTime - Game.RawGameTime;
        }
    }
}