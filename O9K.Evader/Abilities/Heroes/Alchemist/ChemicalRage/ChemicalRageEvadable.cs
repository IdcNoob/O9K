namespace O9K.Evader.Abilities.Heroes.Alchemist.ChemicalRage
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ChemicalRageEvadable : ModifierCounterEvadable
    {
        public ChemicalRageEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 400);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}