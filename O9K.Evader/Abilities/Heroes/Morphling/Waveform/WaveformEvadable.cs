namespace O9K.Evader.Abilities.Heroes.Morphling.Waveform
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class WaveformEvadable : LinearProjectileEvadable
    {
        public WaveformEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo global disable ?

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
        }
    }
}