namespace O9K.Evader.Abilities.Heroes.OutworldDevourer.AstralImprisonment
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class AstralImprisonmentEvadable : TargetableEvadable
    {
        public AstralImprisonmentEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.Bulldoze);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.LotusOrb);
        }
    }
}