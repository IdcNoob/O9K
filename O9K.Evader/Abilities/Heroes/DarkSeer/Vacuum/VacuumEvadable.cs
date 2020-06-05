namespace O9K.Evader.Abilities.Heroes.DarkSeer.Vacuum
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class VacuumEvadable : LinearAreaOfEffectEvadable
    {
        public VacuumEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.Add(Abilities.Armlet);
        }
    }
}