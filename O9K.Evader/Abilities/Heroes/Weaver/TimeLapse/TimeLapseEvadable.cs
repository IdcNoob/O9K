namespace O9K.Evader.Abilities.Heroes.Weaver.TimeLapse
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TimeLapseEvadable : GlobalEvadable
    {
        public TimeLapseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}