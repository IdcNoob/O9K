namespace O9K.Evader.Abilities.Heroes.Slark.Pounce
{
    using System;
    using System.Threading.Tasks;

    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PounceEvadable : LinearProjectileEvadable, IModifierCounter, IParticle
    {
        public PounceEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.HeavenlyGrace);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Invulnerability);

            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.Add(Abilities.IronwoodTree);
            this.ModifierCounters.Add(Abilities.IronBranch);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.UnionWith(Abilities.Invisibility);
            this.ModifierCounters.Add(Abilities.ShadowRealm);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            this.Pathfinder.CancelObstacle(this.Ability.Handle);

            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);

            if (this.Owner.IsVisible)
            {
                var startPosition = this.Owner.Position;

                UpdateManager.BeginInvoke(
                    () =>
                        {
                            try
                            {
                                if (!particle.IsValid)
                                {
                                    return;
                                }

                                var endPosition = particle.GetControlPoint(0);

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
                        },
                    10);
            }
            else
            {
                UpdateManager.BeginInvoke(
                    async () =>
                        {
                            try
                            {
                                if (!particle.IsValid)
                                {
                                    return;
                                }

                                var startPosition = particle.GetControlPoint(0);

                                await Task.Delay(50);

                                if (!particle.IsValid)
                                {
                                    return;
                                }

                                var endPosition = particle.GetControlPoint(0);

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

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}