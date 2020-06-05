namespace O9K.Evader.Abilities.Units.AncientThunderhide.Slam
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class SlamEvadable : AreaOfEffectEvadable
    {
        public SlamEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}