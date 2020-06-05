namespace O9K.Evader.Abilities.Items.ScytheOfVyse
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Proactive;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ScytheOfVyseEvadable : ModifierCounterEvadable, IProactiveCounter
    {
        public ScytheOfVyseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.Add(Abilities.LinkensSphere);
            this.ModifierCounters.Add(Abilities.LotusOrb);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);

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