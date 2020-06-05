namespace O9K.Evader.Abilities.Heroes.Sniper.Assassinate
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class AssassinateEvadable : ProjectileEvadable
    {
        public AssassinateEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo modifier obstacle?

            this.Blinks.UnionWith(Abilities.InstantBlink);
            this.Blinks.Add(Abilities.Waveform);

            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.Add(Abilities.EulsScepterOfDivinity);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Snowball);
            this.Counters.UnionWith(Abilities.MagicImmunity);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.LotusOrb);
        }

        public override bool IsDisjointable { get; } = false;
    }
}