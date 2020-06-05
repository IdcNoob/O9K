namespace O9K.Evader.Abilities.Heroes.DeathProphet.Silence
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class SilenceEvadable : LinearAreaOfEffectEvadable, IModifierCounter
    {
        public SilenceEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Invisibility);

            this.Counters.Remove(Abilities.Enrage);
            this.Counters.Remove(Abilities.EulsScepterOfDivinity);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}