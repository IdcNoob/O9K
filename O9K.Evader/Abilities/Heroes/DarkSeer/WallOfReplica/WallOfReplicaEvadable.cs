namespace O9K.Evader.Abilities.Heroes.DarkSeer.WallOfReplica
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class WallOfReplicaEvadable : GlobalEvadable
    {
        public WallOfReplicaEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}