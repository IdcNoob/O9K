namespace O9K.Evader.Abilities.Heroes.Pugna.LifeDrain
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class LifeDrainEvadable : TargetableEvadable, IModifierCounter
    {
        public LifeDrainEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);

            this.ModifierCounters.Add(Abilities.PhaseShift);
            this.ModifierCounters.Add(Abilities.EulsScepterOfDivinity);
            this.ModifierCounters.Add(Abilities.SleightOfFist);
            this.ModifierCounters.Add(Abilities.BallLightning);
            this.ModifierCounters.UnionWith(Abilities.Shield);
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.MagicShield);
            this.ModifierCounters.Add(Abilities.BladeMail);

            this.ModifierDisables.UnionWith(Abilities.Disable);
            this.ModifierDisables.UnionWith(Abilities.ChannelDisable);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
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