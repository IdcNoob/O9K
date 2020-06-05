namespace O9K.Evader.Abilities.Heroes.WitchDoctor.DeathWard
{
    using Base.Evadable;

    using Ensage;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    using SharpDX;

    internal class DeathWardObstacle : AreaOfEffectObstacle
    {
        public DeathWardObstacle(EvadableAbility ability, Vector3 position, Modifier modifier)
            : base(ability, position)
        {
            this.Modifier = modifier;
        }

        public override bool IsExpired
        {
            get
            {
                return base.IsExpired || !this.Modifier.IsValid || !this.Caster.IsChanneling;
            }
        }

        protected Modifier Modifier { get; }
    }
}