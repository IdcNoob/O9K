namespace O9K.Evader.Abilities.Heroes.Lion.ManaDrain
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ManaDrainEvadable : TargetableEvadable
    {
        public ManaDrainEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
        }
    }
}