namespace O9K.Evader.Abilities.Heroes.QueenOfPain.Blink
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class BlinkEvadable : GlobalEvadable
    {
        public BlinkEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}