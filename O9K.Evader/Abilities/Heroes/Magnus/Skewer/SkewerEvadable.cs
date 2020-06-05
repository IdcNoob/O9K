namespace O9K.Evader.Abilities.Heroes.Magnus.Skewer
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SkewerEvadable : LinearProjectileEvadable
    {
        public SkewerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.Doppelganger);
            this.Counters.Add(Abilities.AttributeShift);
        }
    }
}