namespace O9K.Evader.Abilities.Heroes.NaturesProphet.Sprout
{
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal sealed class SproutEvadable : AreaOfEffectEvadable, IParticle
    {
        public SproutEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.TreeGrab);
            this.Counters.Add(Abilities.QuellingBlade);
            this.Counters.Add(Abilities.BattleFury);
            this.Counters.Add(Abilities.IronTalon);
            this.Counters.UnionWith(Abilities.Tango);
        }

        public override bool CanBeDodged { get; } = false;

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);
            var position = particle.GetControlPoint(0);

            var obstacle = new AreaOfEffectObstacle(this, position)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.Duration
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return true;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}