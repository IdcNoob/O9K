namespace O9K.Evader.Abilities.Heroes.Disruptor.StaticStorm
{
    using Base.Evadable;

    using Core.Entities.Units;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class StaticStormFormationObstacle : AreaOfEffectModifierObstacle
    {
        private readonly float activationTime;

        public StaticStormFormationObstacle(EvadableAbility ability, Vector3 position, Modifier modifier, float activationTime)
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