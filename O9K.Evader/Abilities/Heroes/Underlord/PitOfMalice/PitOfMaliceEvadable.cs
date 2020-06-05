namespace O9K.Evader.Abilities.Heroes.Underlord.PitOfMalice
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PitOfMaliceEvadable : LinearAreaOfEffectEvadable, IModifierCounter
    {
        public PitOfMaliceEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.PhaseShift);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.Add(Abilities.Enrage);
            this.ModifierCounters.Add(Abilities.ChemicalRage);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);

            this.ModifierCounters.Remove(Abilities.EulsScepterOfDivinity);
            this.ModifierCounters.Remove(Abilities.DarkPact);
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