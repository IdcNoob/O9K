namespace O9K.Evader.Abilities.Heroes.FacelessVoid.TimeWalk
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TimeWalkEvadable : GlobalEvadable
    {
        public TimeWalkEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}