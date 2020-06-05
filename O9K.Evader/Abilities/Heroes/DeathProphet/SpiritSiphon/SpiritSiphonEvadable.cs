namespace O9K.Evader.Abilities.Heroes.DeathProphet.SpiritSiphon
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SpiritSiphonEvadable : TargetableEvadable
    {
        public SpiritSiphonEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LotusOrb);
        }
    }
}