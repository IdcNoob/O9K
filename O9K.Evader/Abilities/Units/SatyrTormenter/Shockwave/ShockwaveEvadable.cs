namespace O9K.Evader.Abilities.Units.SatyrTormenter.Shockwave
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class ShockwaveEvadable : ConeProjectileEvadable
    {
        public ShockwaveEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}