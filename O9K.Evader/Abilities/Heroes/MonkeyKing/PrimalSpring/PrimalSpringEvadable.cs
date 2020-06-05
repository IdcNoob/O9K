namespace O9K.Evader.Abilities.Heroes.MonkeyKing.PrimalSpring
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PrimalSpringEvadable : AreaOfEffectEvadable
    {
        public PrimalSpringEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}