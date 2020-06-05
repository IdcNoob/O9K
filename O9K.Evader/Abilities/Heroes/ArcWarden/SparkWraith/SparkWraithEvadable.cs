namespace O9K.Evader.Abilities.Heroes.ArcWarden.SparkWraith
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SparkWraithEvadable : ProjectileEvadable
    {
        public SparkWraithEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.BladeMail);
            this.Counters.Remove(Abilities.Counterspell);
        }

        public override bool IsDisjointable { get; } = false;
    }
}