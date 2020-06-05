namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.Illuminate
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class IlluminateEvadable : LinearProjectileEvadable
    {
        public IlluminateEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}