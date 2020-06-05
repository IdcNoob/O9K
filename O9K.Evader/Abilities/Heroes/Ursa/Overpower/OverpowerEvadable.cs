namespace O9K.Evader.Abilities.Heroes.Ursa.Overpower
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class OverpowerEvadable : ModifierCounterEvadable
    {
        public OverpowerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.Add(Abilities.BladeMail);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 450);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}