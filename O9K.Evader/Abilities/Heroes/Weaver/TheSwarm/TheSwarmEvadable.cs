namespace O9K.Evader.Abilities.Heroes.Weaver.TheSwarm
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TheSwarmEvadable : LinearProjectileEvadable
    {
        public TheSwarmEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo modifier counter?
            this.Counters.Add(Abilities.PhaseShift);
        }
    }
}