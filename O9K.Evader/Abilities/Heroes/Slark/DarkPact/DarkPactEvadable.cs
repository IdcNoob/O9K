namespace O9K.Evader.Abilities.Heroes.Slark.DarkPact
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class DarkPactEvadable : AreaOfEffectEvadable
    {
        public DarkPactEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}