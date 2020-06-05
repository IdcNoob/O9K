namespace O9K.Evader.Abilities.Heroes.Magnus.Empower
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EmpowerEvadable : ModifierCounterEvadable
    {
        public EmpowerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.AllyPurge);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, modifierOwner.GetAttackRange() + 100);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}