namespace O9K.Evader.Abilities.Heroes.PhantomAssassin.StiflingDagger
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class StiflingDaggerEvadable : ProjectileEvadable, IModifierCounter
    {
        public StiflingDaggerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Remove(Abilities.EulsScepterOfDivinity);
            this.ModifierCounters.Remove(Abilities.DarkPact);
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