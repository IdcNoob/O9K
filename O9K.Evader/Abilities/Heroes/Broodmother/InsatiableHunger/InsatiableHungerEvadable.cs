namespace O9K.Evader.Abilities.Heroes.Broodmother.InsatiableHunger
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class InsatiableHungerEvadable : ModifierCounterEvadable
    {
        public InsatiableHungerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
            this.ModifierDisables.UnionWith(Abilities.Root);
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);

            this.ModifierCounters.UnionWith(Abilities.PhysShield);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.Owner.GetAttackRange() + 100);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}