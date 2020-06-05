namespace O9K.Evader.Abilities.Heroes.Medusa.StoneGaze
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class StoneGazeEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public StoneGazeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add modifier counter ?
            this.Disables.UnionWith(Abilities.Disable);

            this.ModifierDisables.UnionWith(Abilities.PhysDisable);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool CanBeDodged { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}