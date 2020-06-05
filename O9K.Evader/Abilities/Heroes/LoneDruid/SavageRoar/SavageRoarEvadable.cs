namespace O9K.Evader.Abilities.Heroes.LoneDruid.SavageRoar
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class SavageRoarEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public SavageRoarEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.EulsScepterOfDivinity);
            this.Counters.Add(Abilities.DarkPact);
            this.Counters.Add(Abilities.Refraction);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.MagicImmunity);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.MagicImmunity);
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