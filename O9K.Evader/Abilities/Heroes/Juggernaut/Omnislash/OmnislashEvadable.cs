namespace O9K.Evader.Abilities.Heroes.Juggernaut.Omnislash
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class OmnislashEvadabe : TargetableEvadable, IModifierCounter
    {
        public OmnislashEvadabe(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.InstantBlink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.UnionWith(Abilities.Invisibility);

            this.Counters.ExceptWith(Abilities.MagicImmunity);
            this.Counters.Remove(Abilities.DarkPact);
            this.Counters.Remove(Abilities.EulsScepterOfDivinity);
            this.Counters.Remove(Abilities.Blur);

            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.Invisibility);
            this.ModifierCounters.Add(Abilities.MantaStyle);
            this.ModifierCounters.Add(Abilities.AttributeShift);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public bool ModifierAllyCounter { get; } = false;

        public bool ModifierEnemyCounter { get; } = true;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierEnemyObstacle(this, modifier, modifierOwner, this.ActiveAbility.Radius);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }
    }
}