namespace O9K.Evader.Abilities.Heroes.ShadowShaman.MassSerpentWard
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class MassSerpentWardEvadable : GlobalEvadable
    {
        public MassSerpentWardEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}