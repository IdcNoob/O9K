namespace O9K.Evader.Abilities.Heroes.NaturesProphet.WrathOfNature
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class WrathOfNatureEvadable : ProjectileEvadable
    {
        public WrathOfNatureEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}