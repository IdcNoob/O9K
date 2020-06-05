namespace O9K.Evader.Abilities.Heroes.Terrorblade.TerrorWave
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class TerrorWaveEvadable : ModifierCounterEvadable
    {
        public TerrorWaveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.Stun);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.Owner.GetAttackRange() + 100);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}