namespace O9K.Evader.Abilities.Heroes.SandKing.Epicenter
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class EpicenterEvadable : GlobalEvadable
    {
        public EpicenterEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}