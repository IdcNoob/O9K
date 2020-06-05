namespace O9K.Evader.Abilities.Heroes.Pugna.Decrepify
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class DecrepifyEvadable : TargetableEvadable, IModifierCounter
    {
        public DecrepifyEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo ally counter?

            this.Counters.Add(Abilities.Counterspell);

            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 600);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}