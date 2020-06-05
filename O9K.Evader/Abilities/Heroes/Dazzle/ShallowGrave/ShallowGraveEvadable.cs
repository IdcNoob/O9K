namespace O9K.Evader.Abilities.Heroes.Dazzle.ShallowGrave
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ShallowGraveEvadable : GlobalEvadable, IModifierCounter
    {
        public ShallowGraveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.UnionWith(Abilities.Root);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 1000);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}