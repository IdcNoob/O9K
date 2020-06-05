namespace O9K.Evader.Abilities.Heroes.EmberSpirit.SleightOfFist
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SleightOfFistEvadable : LinearAreaOfEffectEvadable
    {
        public SleightOfFistEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            // this.Counters.Add(Abilities.Bulwark);
        }
    }
}