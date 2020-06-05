namespace O9K.Evader.Abilities.Items.HealingSalve
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    internal sealed class HealingSalveEvadable : ModifierCounterEvadable
    {
        public HealingSalveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.UnionWith(Abilities.FlaskNukes);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new HealingSalveEvadableModifier(this, modifier, modifierOwner, 2000);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}