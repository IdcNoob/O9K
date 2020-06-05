namespace O9K.Evader.Abilities.Heroes.Techies.ProximityMines
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class ProximityMinesEvadable : AreaOfEffectEvadable
    {
        public ProximityMinesEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}