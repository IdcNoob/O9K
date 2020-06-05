namespace O9K.Evader.Abilities.Heroes.FacelessVoid.Chronosphere
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;

    using SharpDX;

    internal class ChronosphereLinearAllyObstacle : LinearAreaOfEffectObstacle
    {
        public ChronosphereLinearAllyObstacle(LinearAreaOfEffectEvadable ability, Vector3 startPosition)
            : base(ability, startPosition)
        {
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (this.Caster.Equals(unit))
            {
                return false;
            }

            return base.IsIntersecting(unit, checkPrediction);
        }
    }
}