namespace O9K.Evader.Abilities.Heroes.Phoenix.IcarusDive
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class IcarusDiveEvadable : GlobalEvadable
    {
        public IcarusDiveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo disable when flying, remove projectile disables
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}