namespace O9K.Evader.Abilities.Heroes.Lina.LagunaBlade
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class LagunaBladeEvadable : TargetableEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> aghanimCounters = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> aghanimModifierCounters = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> counters = new HashSet<AbilityId>();

        private readonly HashSet<AbilityId> modifierCounters = new HashSet<AbilityId>();

        public LagunaBladeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.counters.Add(Abilities.Counterspell);
            this.counters.Add(Abilities.LinkensSphere);
            this.counters.Add(Abilities.BallLightning);
            this.counters.UnionWith(Abilities.Invulnerability);
            this.counters.Add(Abilities.TricksOfTheTrade);
            this.counters.Add(Abilities.AttributeShift);
            this.counters.UnionWith(Abilities.StrongMagicShield);
            this.counters.Add(Abilities.Armlet);
            this.counters.UnionWith(Abilities.Heal);
            this.counters.UnionWith(Abilities.Suicide);
            this.counters.Add(Abilities.LotusOrb);

            this.aghanimCounters.Add(Abilities.Counterspell);
            this.aghanimCounters.Add(Abilities.LinkensSphere);
            this.aghanimCounters.Add(Abilities.BallLightning);
            this.aghanimCounters.UnionWith(Abilities.Invulnerability);
            this.aghanimCounters.Add(Abilities.TricksOfTheTrade);
            this.aghanimCounters.Add(Abilities.AttributeShift);
            this.aghanimCounters.Add(Abilities.Armlet);
            this.aghanimCounters.UnionWith(Abilities.Heal);
            this.aghanimCounters.UnionWith(Abilities.Suicide);
            this.aghanimCounters.Add(Abilities.LotusOrb);
            this.aghanimCounters.Add(Abilities.CourierShield);

            this.modifierCounters.Add(Abilities.SpikedCarapace);
            this.modifierCounters.Add(Abilities.Refraction);
            this.modifierCounters.Add(Abilities.SleightOfFist);
            this.modifierCounters.Add(Abilities.PhaseShift);
            this.modifierCounters.Add(Abilities.Snowball);
            this.modifierCounters.Add(Abilities.Enrage);
            this.modifierCounters.UnionWith(Abilities.MagicImmunity);
            this.modifierCounters.Add(Abilities.Mischief);
            this.modifierCounters.Add(Abilities.MantaStyle);
            this.modifierCounters.Add(Abilities.EulsScepterOfDivinity);
            this.modifierCounters.Add(Abilities.BladeMail);

            this.aghanimModifierCounters.Add(Abilities.SpikedCarapace);
            this.aghanimModifierCounters.Add(Abilities.Refraction);
            this.aghanimModifierCounters.Add(Abilities.SleightOfFist);
            this.aghanimModifierCounters.Add(Abilities.PhaseShift);
            this.aghanimModifierCounters.Add(Abilities.Snowball);
            this.aghanimModifierCounters.Add(Abilities.Mischief);
            this.aghanimModifierCounters.Add(Abilities.MantaStyle);
            this.aghanimModifierCounters.Add(Abilities.EulsScepterOfDivinity);
            this.aghanimModifierCounters.Add(Abilities.Enrage);
            this.aghanimModifierCounters.Add(Abilities.BladeMail);
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

        public override HashSet<AbilityId> ModifierCounters
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.aghanimModifierCounters;
                }

                return this.modifierCounters;
            }
        }

        public bool ModifierEnemyCounter { get; } = false;

        protected override IEnumerable<AbilityId> AllCounters
        {
            get
            {
                return this.aghanimCounters.Concat(this.counters);
            }
        }

        protected override IEnumerable<AbilityId> AllModifierCounters
        {
            get
            {
                return this.modifierCounters.Concat(this.aghanimCounters);
            }
        }

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return false;
        }

        protected override void AddObstacle()
        {
            var obstacle = new LagunaBladeObstacle(this)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}