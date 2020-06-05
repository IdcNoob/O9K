namespace O9K.Evader.Abilities.Heroes.Disruptor.KineticField
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class KineticFieldObstacle : AreaOfEffectModifierObstacle
    {
        private readonly float activationTime;

        public KineticFieldObstacle(EvadableAbility ability, Vector3 position, Modifier modifier, float activationTime)
            : base(ability, position, modifier)
        {
            this.activationTime = activationTime;
            this.CanBeDodged = false;
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!this.Modifier.IsValid)
            {
                return false;
            }

            if (this.Modifier.ElapsedTime < this.activationTime)
            {
                return false;
            }

            return base.IsIntersecting(unit, checkPrediction);
        }
    }
}