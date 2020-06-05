namespace O9K.Evader.Abilities.Heroes.TreantProtector.Overgrowth
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class OvergrowthEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public OvergrowthEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.SlowHeal);

            this.Counters.Remove(Abilities.Enrage);
            this.Counters.ExceptWith(Abilities.MagicImmunity);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.Add(Abilities.Enrage);
            this.ModifierCounters.Add(Abilities.ChemicalRage);
            this.ModifierCounters.UnionWith(Abilities.MagicImmunity);
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