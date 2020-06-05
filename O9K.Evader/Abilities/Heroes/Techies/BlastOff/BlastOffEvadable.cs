namespace O9K.Evader.Abilities.Heroes.Techies.BlastOff
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

    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    using SharpDX;

    internal sealed class BlastOffEvadable : LinearAreaOfEffectEvadable, IModifierCounter, IModifierObstacle, IParticle
    {
        private Vector3 startPosition = Vector3.Zero;

        private float startTime;

        public BlastOffEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
        }

        public bool AllyModifierObstacle { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            if (this.startPosition.IsZero)
            {
                return;
            }

            var obstacle = new LinearAreaOfEffectObstacle(this, this.startPosition)
            {
                EndCastTime = this.startTime,
                EndObstacleTime = this.startTime + this.Ability.ActivationDelay,
            };

            this.Pathfinder.AddObstacle(obstacle);
            this.startPosition = Vector3.Zero;
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            if (this.Owner.IsVisible)
            {
                return;
            }

            this.startTime = Game.RawGameTime - (Game.Ping / 2000);

            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            if (!particle.IsValid)
                            {
                                return;
                            }

                            this.startPosition = particle.GetControlPoint(1);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }
    }
}