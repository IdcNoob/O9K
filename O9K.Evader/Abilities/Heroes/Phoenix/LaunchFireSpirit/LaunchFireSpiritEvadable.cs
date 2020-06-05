namespace O9K.Evader.Abilities.Heroes.Phoenix.LaunchFireSpirit
{
    using System;

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

    internal sealed class LaunchFireSpiritEvadable : LinearProjectileEvadable, IParticle, IModifierCounter
    {
        public LaunchFireSpiritEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            if (name.Contains("ground"))
            {
                this.Pathfinder.CancelObstacle(this.Ability.Handle, false, true);
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

                            var start = particle.GetControlPoint(0);
                            var spirit = particle.GetControlPoint(1);
                            var direction = spirit + start;

                            var obstacle = new LinearProjectileObstacle(this, start, direction)
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