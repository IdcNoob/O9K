namespace O9K.Evader.Abilities.Heroes.Axe.BerserkersCall
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BerserkersCallEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public BerserkersCallEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.Invulnerability);
            this.Disables.UnionWith(Abilities.Break);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.Satanic);
            this.Counters.Add(Abilities.SilverEdge);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.GlimmerCape);

            this.ModifierDisables.UnionWith(Abilities.Invulnerability);
            this.ModifierDisables.UnionWith(Abilities.Break);

            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
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