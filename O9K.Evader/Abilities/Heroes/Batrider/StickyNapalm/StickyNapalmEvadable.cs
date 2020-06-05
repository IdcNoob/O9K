namespace O9K.Evader.Abilities.Heroes.Batrider.StickyNapalm
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class StickyNapalmEvadable : ModifierCounterEvadable
    {
        public StickyNapalmEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo linear aoe evade ?

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierStackAllyObstacle(this, modifier, modifierOwner, 5);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}