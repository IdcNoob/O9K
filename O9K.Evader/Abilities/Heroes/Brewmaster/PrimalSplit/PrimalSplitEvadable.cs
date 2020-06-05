namespace O9K.Evader.Abilities.Heroes.Brewmaster.PrimalSplit
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PrimalSplitEvadable : GlobalEvadable
    {
        public PrimalSplitEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}