namespace O9K.Evader.Abilities.Heroes.Sven.GodsStrength
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class GodsStrengthEvadable : GlobalEvadable, IModifierCounter
    {
        public GodsStrengthEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.ModifierDisables.UnionWith(Abilities.Stun);
            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.UnionWith(Abilities.EnemyStrongPurge);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.Add(Abilities.BladeMail);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.Owner.GetAttackRange() + 250);
            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}