namespace O9K.Evader.Abilities.Heroes.Lina.LagunaBlade
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.Targetable;

    internal class LagunaBladeObstacle : TargetableObstacle
    {
        public LagunaBladeObstacle(EvadableAbility ability, float radius = 75)
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