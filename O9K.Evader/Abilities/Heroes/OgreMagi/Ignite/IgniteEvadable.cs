namespace O9K.Evader.Abilities.Heroes.OgreMagi.Ignite
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class IgniteEvadable : ProjectileEvadable, IModifierCounter
    {
        public IgniteEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Shield);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}