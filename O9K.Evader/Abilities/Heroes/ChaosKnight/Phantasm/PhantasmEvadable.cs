namespace O9K.Evader.Abilities.Heroes.ChaosKnight.Phantasm
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PhantasmEvadable : GlobalEvadable
    {
        public PhantasmEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}