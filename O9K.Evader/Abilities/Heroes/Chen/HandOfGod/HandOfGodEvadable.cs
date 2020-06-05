namespace O9K.Evader.Abilities.Heroes.Chen.HandOfGod
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class HandOfGodEvadable : GlobalEvadable
    {
        public HandOfGodEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}