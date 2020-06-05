namespace O9K.Evader.Abilities.Heroes.Ursa.Enrage
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EnrageEvadable : ModifierCounterEvadable
    {
        public EnrageEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);
            this.ModifierDisables.UnionWith(Abilities.Stun);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
            this.ModifierDisables.UnionWith(Abilities.Root);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 450);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}