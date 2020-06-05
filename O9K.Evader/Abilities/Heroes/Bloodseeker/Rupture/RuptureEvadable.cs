namespace O9K.Evader.Abilities.Heroes.Bloodseeker.Rupture
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class RuptureEvadable : TargetableEvadable, IModifierCounter
    {
        public RuptureEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Invisibility);

            this.Counters.Remove(Abilities.Enrage);

            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.Add(Abilities.BladeMail);

            this.ModifierDisables.UnionWith(Abilities.PhysDisable);
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