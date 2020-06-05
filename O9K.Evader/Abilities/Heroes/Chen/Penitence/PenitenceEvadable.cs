namespace O9K.Evader.Abilities.Heroes.Chen.Penitence
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PenitenceEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        public PenitenceEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.Shukuchi);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.DarkPact);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.Doppelganger);
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