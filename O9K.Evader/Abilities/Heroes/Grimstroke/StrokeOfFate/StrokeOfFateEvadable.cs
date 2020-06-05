namespace O9K.Evader.Abilities.Heroes.Grimstroke.StrokeOfFate
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

    internal sealed class StrokeOfFateEvadable : LinearProjectileEvadable, IParticle, IModifierCounter
    {
        public StrokeOfFateEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.Spoink);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
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
                            var endPosition = particle.GetControlPoint(5);

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