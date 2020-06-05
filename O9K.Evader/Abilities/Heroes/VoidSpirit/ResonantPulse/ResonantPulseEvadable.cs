namespace O9K.Evader.Abilities.Heroes.VoidSpirit.ResonantPulse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Logger;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ResonantPulseEvadable : AreaOfEffectEvadable, IModifierCounter, IParticle
    {
        private readonly HashSet<AbilityId> aghanimBlinks = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> aghanimCounters = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> counters = new HashSet<AbilityId>();

        public ResonantPulseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.aghanimBlinks.UnionWith(Abilities.Blink);

            this.counters.Add(Abilities.AttributeShift);
            this.counters.UnionWith(Abilities.Shield);
            this.counters.UnionWith(Abilities.StrongMagicShield);
            this.counters.UnionWith(Abilities.Heal);
            this.counters.Add(Abilities.Armlet);
            this.counters.UnionWith(Abilities.Suicide);
            this.counters.Add(Abilities.BladeMail);

            this.aghanimCounters.Add(Abilities.AttributeShift);
            this.aghanimCounters.UnionWith(Abilities.MagicImmunity);
            this.aghanimCounters.UnionWith(Abilities.Shield);
            this.aghanimCounters.UnionWith(Abilities.StrongMagicShield);
            this.aghanimCounters.UnionWith(Abilities.Heal);
            this.aghanimCounters.Add(Abilities.Armlet);
            this.aghanimCounters.UnionWith(Abilities.Suicide);
            this.aghanimCounters.Add(Abilities.BladeMail);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public override HashSet<AbilityId> Blinks
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.aghanimBlinks;
                }

                return base.Blinks;
            }
        }

        public override HashSet<AbilityId> Counters
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.aghanimCounters;
                }

                return this.counters;
            }
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        protected override IEnumerable<AbilityId> AllBlinks
        {
            get
            {
                return this.aghanimBlinks;
            }
        }

        protected override IEnumerable<AbilityId> AllCounters
        {
            get
            {
                return this.aghanimCounters.Concat(this.counters);
            }
        }

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

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
                            var obstacle = new AreaOfEffectSpeedObstacle(this, position, 200, 200)
                            {
                                EndCastTime = time,
                                EndObstacleTime = time + (this.ActiveAbility.Radius / this.ActiveAbility.Speed)
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