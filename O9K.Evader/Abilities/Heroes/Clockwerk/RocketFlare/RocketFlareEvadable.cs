namespace O9K.Evader.Abilities.Heroes.Clockwerk.RocketFlare
{
    using System;

    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal sealed class RocketFlareEvadable : AreaOfEffectEvadable, IParticle
    {
        public RocketFlareEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            if (name.Contains("illumination"))
            {
                return;
            }

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

                            var startPosition = particle.GetControlPoint(0);
                            var endPosition = particle.GetControlPoint(1);

                            var obstacle = new AreaOfEffectObstacle(this, endPosition)
                            {
                                EndCastTime = time,
                                EndObstacleTime = (time + (startPosition.Distance2D(endPosition) / this.ActiveAbility.Speed)) - 0.1f
                            };

                            this.Pathfinder.AddObstacle(obstacle);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}