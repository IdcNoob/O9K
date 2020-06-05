namespace O9K.Evader.Abilities.Heroes.Grimstroke.Soulbind
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class SoulbindEvadable : TargetableEvadable, IModifierCounter
    {
        public SoulbindEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add aoe manta dodge

            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Bulldoze);

            this.ModifierCounters.Add(Abilities.Refraction);
            this.ModifierCounters.Add(Abilities.Snowball);
            this.ModifierCounters.UnionWith(Abilities.MagicImmunity);
            this.ModifierCounters.Add(Abilities.AttributeShift);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
            this.ModifierCounters.Add(Abilities.BladeMail);
            this.ModifierCounters.Add(Abilities.LotusOrb);
            this.ModifierCounters.Add(Abilities.SpikedCarapace);

            this.ModifierCounters.Remove(Abilities.Bristleback);
            this.ModifierCounters.Remove(Abilities.BlackKingBar);
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