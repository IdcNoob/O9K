namespace O9K.Evader.Abilities.Items.Bloodthorn
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Proactive;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BloodthornEvadable : ModifierCounterEvadable, IProactiveCounter
    {
        public BloodthornEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);

            this.ProactiveBlinks.UnionWith(Abilities.ProactiveBlink);

            this.ProactiveDisables.UnionWith(Abilities.ProactiveItemDisable);

            this.ProactiveCounters.UnionWith(Abilities.ProactiveHexCounter);
        }

        public override bool ModifierAllyCounter { get; } = true;

        public override void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddProactiveObstacle()
        {
            var obstacle = new ProactiveAbilityObstacle(this);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}