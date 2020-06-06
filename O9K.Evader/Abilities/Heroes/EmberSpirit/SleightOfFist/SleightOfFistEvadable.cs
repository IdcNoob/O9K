namespace O9K.Evader.Abilities.Heroes.EmberSpirit.SleightOfFist
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class SleightOfFistEvadable : ModifierCounterEvadable
    {
        public SleightOfFistEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.Add(Abilities.PhaseShift);
            this.ModifierCounters.Add(Abilities.Doppelganger);
            this.Counters.UnionWith(Abilities.PhysShield);
            this.ModifierCounters.Add(Abilities.Armlet);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}