namespace O9K.Evader.Abilities.Heroes.Abaddon.AphoticShield
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class AphoticShieldEvadable : ModifierCounterEvadable
    {
        public AphoticShieldEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierDisables.UnionWith(Abilities.EnemyPurge);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, 1000);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}