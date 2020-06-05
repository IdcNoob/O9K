namespace O9K.Evader.Abilities.Heroes.Pudge.MeatHook
{
    using System;

    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;

    internal sealed class MeatHookEvadable : LinearProjectileEvadable, IParticle
    {
        public MeatHookEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.EulsScepterOfDivinity);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            if (name.Contains("impact"))
            {
                this.Pathfinder.CancelObstacle(this.Ability.Handle);
                return;
            }

            if (this.Owner.IsVisible)
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

                            var obstacle = new LinearProjectileObstacle(this, startPosition, endPosition)
                            {
                                EndCastTime = time,
                                EndObstacleTime = time + (this.RangedAbility.Range / this.RangedAbility.Speed)
                            };

                            this.Pathfinder.AddObstacle(obstacle);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }
    }
}