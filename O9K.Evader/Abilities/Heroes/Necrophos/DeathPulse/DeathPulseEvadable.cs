namespace O9K.Evader.Abilities.Heroes.Necrophos.DeathPulse
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class DeathPulseEvadable : ProjectileEvadable
    {
        public DeathPulseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.Add(Abilities.GlimmerCape);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.Counterspell);
        }

        public override bool IsDisjointable { get; } = false;
    }
}