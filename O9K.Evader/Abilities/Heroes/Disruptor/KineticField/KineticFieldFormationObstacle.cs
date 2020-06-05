namespace O9K.Evader.Abilities.Heroes.Disruptor.KineticField
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class KineticFieldFormationObstacle : AreaOfEffectModifierObstacle
    {
        private readonly float activationTime;

        public KineticFieldFormationObstacle(EvadableAbility ability, Vector3 position, Modifier modifier, float activationTime)
            : base(ability, position, modifier)
        {
            this.activationTime = activationTime;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.Modifier.IsValid || this.activationTime - this.Modifier.ElapsedTime <= 0;
            }
        }

        public override IEnumerable<AbilityId> GetCounters(Unit9 ally)
        {
            return Enumerable.Empty<AbilityId>();
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            return this.activationTime - this.Modifier.ElapsedTime;
        }
    }
}