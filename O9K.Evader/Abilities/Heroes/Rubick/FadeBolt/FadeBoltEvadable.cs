namespace O9K.Evader.Abilities.Heroes.Rubick.FadeBolt
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class FadeBoltEvadable : TargetableEvadable
    {
        public FadeBoltEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
        }
    }
}