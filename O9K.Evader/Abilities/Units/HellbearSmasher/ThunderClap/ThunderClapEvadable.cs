namespace O9K.Evader.Abilities.Units.HellbearSmasher.ThunderClap
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class ThunderClapEvadable : AreaOfEffectEvadable
    {
        public ThunderClapEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}