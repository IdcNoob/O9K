namespace O9K.Evader.Abilities.Heroes.Abaddon.MistCoil
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class MistCoilEvadable : ProjectileEvadable
    {
        public MistCoilEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.Shukuchi);
            this.Counters.Add(Abilities.ShadowRealm);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.Add(Abilities.GlimmerCape);
            this.Counters.UnionWith(Abilities.Suicide);
        }
    }
}