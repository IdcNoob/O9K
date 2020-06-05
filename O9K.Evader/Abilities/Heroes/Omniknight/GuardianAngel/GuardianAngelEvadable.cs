namespace O9K.Evader.Abilities.Heroes.Omniknight.GuardianAngel
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class GuardianAngelEvadable : GlobalEvadable, IModifierCounter
    {
        public GuardianAngelEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 750);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}