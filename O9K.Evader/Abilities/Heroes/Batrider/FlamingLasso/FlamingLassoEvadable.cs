namespace O9K.Evader.Abilities.Heroes.Batrider.FlamingLasso
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class FlamingLassoEvadable : TargetableEvadable, IModifierCounter
    {
        public FlamingLassoEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);

            this.ModifierDisables.UnionWith(Abilities.Stun);
            this.ModifierDisables.UnionWith(Abilities.Root);
            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}