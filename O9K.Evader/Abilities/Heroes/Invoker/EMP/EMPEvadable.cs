namespace O9K.Evader.Abilities.Heroes.Invoker.EMP
{
    using System;

    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    // ReSharper disable once InconsistentNaming
    internal sealed class EMPEvadable : LinearAreaOfEffectEvadable, IParticle
    {
        public EMPEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
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

            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            if (!particle.IsValid)
                            {
                                return;
                            }

                            var position = particle.GetControlPoint(0);

                            var obstacle = new AreaOfEffectObstacle(this, position)
                            {
                                EndCastTime = time,
                                EndObstacleTime = time + this.Ability.ActivationDelay
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