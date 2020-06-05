namespace O9K.Evader.Abilities.Heroes.Huskar.LifeBreak
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class LifeBreakEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        public LifeBreakEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo fix projectile update while flying with wtf mode ?

            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.Swiftslash);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);

            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
        }

        public override bool IsDisjointable { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}