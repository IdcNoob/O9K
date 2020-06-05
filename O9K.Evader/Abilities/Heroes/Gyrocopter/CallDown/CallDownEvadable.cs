namespace O9K.Evader.Abilities.Heroes.Gyrocopter.CallDown
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal sealed class CallDownEvadable : AreaOfEffectEvadable, IParticle
    {
        public CallDownEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.ShadowRealm);
        }

        public override bool CanBeDodged { get; } = false;

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);
            var position = particle.GetControlPoint(1);

            var obstacle = new AreaOfEffectObstacle(this, position)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);

            var obstacle2 = new AreaOfEffectObstacle(this, position)
            {
                EndCastTime = time,
                EndObstacleTime = time + (this.Ability.ActivationDelay * 2)
            };

            this.Pathfinder.AddObstacle(obstacle2);
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}