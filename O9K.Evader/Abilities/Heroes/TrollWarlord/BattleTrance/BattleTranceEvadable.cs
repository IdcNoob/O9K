namespace O9K.Evader.Abilities.Heroes.TrollWarlord.BattleTrance
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BattleTranceEvadable : ModifierCounterEvadable
    {
        public BattleTranceEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
            this.ModifierDisables.UnionWith(Abilities.Root);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 600);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}