namespace O9K.Evader.Abilities.Items.Clarity
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    internal sealed class ClarityEvadable : ModifierCounterEvadable
    {
        public ClarityEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.UnionWith(Abilities.ClarityNukes);
        }

        public override bool ModifierEnemyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ClarityEvadableModifier(this, modifier, modifierOwner, 2000);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}