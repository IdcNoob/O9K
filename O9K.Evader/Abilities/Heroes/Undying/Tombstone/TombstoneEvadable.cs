namespace O9K.Evader.Abilities.Heroes.Undying.Tombstone
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TombstoneEvadable : GlobalEvadable
    {
        public TombstoneEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}