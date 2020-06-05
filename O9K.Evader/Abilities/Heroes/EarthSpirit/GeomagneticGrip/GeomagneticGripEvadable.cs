namespace O9K.Evader.Abilities.Heroes.EarthSpirit.GeomagneticGrip
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class GeomagneticGripEvadable : LinearAreaOfEffectEvadable
    {
        public GeomagneticGripEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}