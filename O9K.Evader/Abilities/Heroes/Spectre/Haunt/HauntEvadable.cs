namespace O9K.Evader.Abilities.Heroes.Spectre.Haunt
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class HauntEvadable : GlobalEvadable
    {
        public HauntEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}