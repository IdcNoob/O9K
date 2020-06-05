namespace O9K.Evader.Abilities.Heroes.SkywrathMage.ConcussiveShot
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ConcussiveShotEvadable : ProjectileEvadable, IModifierCounter
    {
        public ConcussiveShotEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.Counterspell);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
            this.ModifierCounters.Remove(Abilities.EulsScepterOfDivinity);
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