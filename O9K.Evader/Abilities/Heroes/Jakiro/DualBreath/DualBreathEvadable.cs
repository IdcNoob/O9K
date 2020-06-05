namespace O9K.Evader.Abilities.Heroes.Jakiro.DualBreath
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class DualBreathEvadable : ConeProjectileEvadable, IModifierCounter
    {
        public DualBreathEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Shield);

            this.Counters.Remove(Abilities.Refraction);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
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