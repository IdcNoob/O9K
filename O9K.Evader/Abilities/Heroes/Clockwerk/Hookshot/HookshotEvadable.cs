namespace O9K.Evader.Abilities.Heroes.Clockwerk.Hookshot
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

    internal sealed class HookshotEvadable : LinearProjectileEvadable, IModifierCounter, IParticle
    {
        public HookshotEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
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