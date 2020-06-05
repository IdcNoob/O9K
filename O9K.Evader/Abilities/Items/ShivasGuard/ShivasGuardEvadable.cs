namespace O9K.Evader.Abilities.Items.ShivasGuard
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ShivasGuardEvadable : AreaOfEffectEvadable
    {
        public ShivasGuardEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}