namespace O9K.Evader.Abilities.Heroes.Disruptor.StaticStorm
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class StaticStormObstacle : AreaOfEffectModifierObstacle
    {
        private readonly float activationTime;

        public StaticStormObstacle(EvadableAbility ability, Vector3 position, Modifier modifier, float activationTime)
            : base(ability, position, modifier)
        {
            this.activationTime = activationTime;
        }

        public override IEnumerable<AbilityId> GetBlinks(Unit9 ally)
        {
            return Enumerable.Empty<AbilityId>();
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