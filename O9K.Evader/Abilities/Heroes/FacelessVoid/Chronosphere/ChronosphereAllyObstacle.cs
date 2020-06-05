namespace O9K.Evader.Abilities.Heroes.FacelessVoid.Chronosphere
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using SharpDX;

    internal class ChronosphereAllyObstacle : ChronosphereObstacle
    {
        public ChronosphereAllyObstacle(EvadableAbility ability, Vector3 position, Modifier modifier)
            : base(ability, position, modifier)
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