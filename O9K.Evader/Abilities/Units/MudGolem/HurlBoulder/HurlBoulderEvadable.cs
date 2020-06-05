namespace O9K.Evader.Abilities.Units.MudGolem.HurlBoulder
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class HurlBoulderEvadable : TargetableProjectileEvadable
    {
        public HurlBoulderEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.Shukuchi);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.Invisibility);
        }
    }
}