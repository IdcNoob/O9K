namespace O9K.Evader.Abilities.Heroes.Razor.PlasmaField
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PlasmaFieldEvadable : AreaOfEffectEvadable
    {
        public PlasmaFieldEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}