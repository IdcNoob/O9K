namespace O9K.Evader.Abilities.Heroes.Disruptor.Glimpse
{
    using System;
    using System.Linq;

    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.Proactive;

    internal sealed class GlimpseEvadable : EvadableAbility, IParticle, IProactiveCounter
    {
        public GlimpseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.Add(Abilities.Burrowstrike);
            this.Blinks.Add(Abilities.FireRemnant);

            this.Counters.Add(Abilities.Doppelganger);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.XMark);
            this.Counters.UnionWith(Abilities.MagicImmunity);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.TricksOfTheTrade);
            this.Counters.Add(Abilities.Snowball);
            this.Counters.UnionWith(Abilities.MagicShield);

            this.Counters.Remove(Abilities.Bristleback);

            this.ProactiveCounters.Add(Abilities.Counterspell);
        }

        public override bool CanBeDodged { get; } = false;

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);

            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            if (!particle.IsValid)
                            {
                                return;
                            }

                            var targetPosition = particle.GetControlPoint(0);
                            var target = EntityManager9.Units
                                .Where(x => x.IsAlive && x.IsEnemy(this.Owner) && x.Distance(targetPosition) < 200)
                                .OrderBy(x => x.Distance(targetPosition))
                                .FirstOrDefault();

                            if (target == null)
                            {
                                return;
                            }

                            var obstacle = new GlimpseObstacle(this, target)
                            {
                                EndCastTime = time,
                                EndObstacleTime = time + particle.GetControlPoint(2).X
                            };

                            this.Pathfinder.AddObstacle(obstacle);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }

        public void AddProactiveObstacle()
        {
            var obstacle = new ProactiveAbilityObstacle(this);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return usableAbility?.Ability.Id == Abilities.XMark;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }

        protected override void AddObstacle()
        {
        }
    }
}