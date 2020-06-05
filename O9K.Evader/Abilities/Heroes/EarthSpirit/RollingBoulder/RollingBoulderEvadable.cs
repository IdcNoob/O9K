namespace O9K.Evader.Abilities.Heroes.EarthSpirit.RollingBoulder
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class RollingBoulderEvadable : LinearProjectileEvadable
    {
        public RollingBoulderEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo fix stone speed ?
            this.Counters.Add(Abilities.PhaseShift);
        }
    }
}