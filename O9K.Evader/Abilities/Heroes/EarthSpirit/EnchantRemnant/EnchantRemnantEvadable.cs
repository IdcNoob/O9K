namespace O9K.Evader.Abilities.Heroes.EarthSpirit.EnchantRemnant
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class EnchantRemnantEvadable : TargetableEvadable
    {
        public EnchantRemnantEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.Doppelganger);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.UnionWith(Abilities.MagicImmunity);
            this.Counters.Add(Abilities.LotusOrb);
        }
    }
}