namespace O9K.Evader.Abilities.Heroes.Luna.Eclipse
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EclipseEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public EclipseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.Invulnerability);

            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.ActiveAbility.Radius);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}