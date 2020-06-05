namespace O9K.Evader.Abilities.Heroes.Mars.ArenaOfBlood
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ArenaOfBloodEvadable : LinearAreaOfEffectEvadable
    {
        public ArenaOfBloodEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.UnionWith(Abilities.StrongPhysShield);
        }
    }
}