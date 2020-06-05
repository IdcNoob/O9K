namespace O9K.Evader.Abilities.Heroes.Alchemist.UnstableConcoctionThrow
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class UnstableConcoctionThrowEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        public UnstableConcoctionThrowEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.PhysDisable);

            this.Counters.UnionWith(Abilities.VsDisableProjectile);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.SlowHeal);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool IsDisjointable { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}