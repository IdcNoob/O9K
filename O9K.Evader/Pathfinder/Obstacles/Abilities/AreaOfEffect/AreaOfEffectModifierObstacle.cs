namespace O9K.Evader.Pathfinder.Obstacles.Abilities.AreaOfEffect
{
    using Core.Entities.Units;

    using Ensage;

    using O9K.Evader.Abilities.Base.Evadable;

    using SharpDX;

    internal class AreaOfEffectModifierObstacle : AreaOfEffectObstacle
    {
        public AreaOfEffectModifierObstacle(EvadableAbility ability, Vector3 position, Modifier modifier, int radiusIncrease = 50)
            : base(ability, position, radiusIncrease)
        {
            this.Modifier = modifier;
            this.Delay = Game.Ping / 2000f;
        }

        public override bool IsExpired
        {
            get
            {
                return !this.Modifier.IsValid || this.Modifier.RemainingTime <= 0;
            }
        }

        protected float Delay { get; }

        protected Modifier Modifier { get; }

        public override float GetDisableTime(Unit9 enemy)
        {
            return 0;
        }

        public override float GetEvadeTime(Unit9 ally, bool blink)
        {
            if (!this.Modifier.IsValid)
            {
                return 0;
            }

            return this.Modifier.RemainingTime - this.Delay;
        }
    }
}