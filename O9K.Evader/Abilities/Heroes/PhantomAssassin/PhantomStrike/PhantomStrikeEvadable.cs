namespace O9K.Evader.Abilities.Heroes.PhantomAssassin.PhantomStrike
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PhantomStrikeEvadable : GlobalEvadable, IModifierCounter
    {
        public PhantomStrikeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.ModifierCounters.Add(Abilities.HurricanePike);
            this.ModifierCounters.UnionWith(Abilities.PhysShield);

            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.Owner.GetAttackRange() + 100);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}