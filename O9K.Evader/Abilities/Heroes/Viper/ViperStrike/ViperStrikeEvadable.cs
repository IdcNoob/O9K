namespace O9K.Evader.Abilities.Heroes.Viper.ViperStrike
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ViperStrikeEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        public ViperStrikeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.Shukuchi);
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.Add(Abilities.BladeMail);

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