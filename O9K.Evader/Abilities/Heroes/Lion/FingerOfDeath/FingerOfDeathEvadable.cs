namespace O9K.Evader.Abilities.Heroes.Lion.FingerOfDeath
{
    using Base;
    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class FingerOfDeathEvadable : TargetableEvadable, IModifierCounter
    {
        public FingerOfDeathEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.TricksOfTheTrade);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.LotusOrb);

            this.ModifierCounters.Add(Abilities.SpikedCarapace);
            this.ModifierCounters.Add(Abilities.Refraction);
            this.ModifierCounters.Add(Abilities.SleightOfFist);
            this.ModifierCounters.Add(Abilities.PhaseShift);
            this.ModifierCounters.Add(Abilities.Snowball);
            this.ModifierCounters.Add(Abilities.Enrage);
            this.ModifierCounters.UnionWith(Abilities.MagicImmunity);
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.Add(Abilities.Mischief);
            this.ModifierCounters.Add(Abilities.EulsScepterOfDivinity);
            this.ModifierCounters.Add(Abilities.BladeMail);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

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
            var obstacle = new FingerOfDeathObstacle(this)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}