namespace O9K.Evader.Abilities.Items.Nullifier
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class NullifierEvadable : ProjectileEvadable
    {
        public NullifierEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.UnionWith(Abilities.VsDisableProjectile);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.Add(Abilities.Armlet);

            this.Counters.Remove(Abilities.Enrage);
            this.Counters.Remove(Abilities.ShadowDance);
            this.Counters.Remove(Abilities.SpikedCarapace);
            this.Counters.Remove(Abilities.Refraction);
            this.Counters.Remove(Abilities.ShadowRealm);
            this.Counters.Remove(Abilities.ShadowRealm);
            this.Counters.Remove(Abilities.LifeBreak);
        }
    }
}