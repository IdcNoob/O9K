namespace O9K.Evader.Abilities.Heroes.VengefulSpirit.NetherSwap
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class NetherSwapEvadable : TargetableEvadable
    {
        public NetherSwapEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.InstantBlink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.MantaStyle);
        }
    }
}