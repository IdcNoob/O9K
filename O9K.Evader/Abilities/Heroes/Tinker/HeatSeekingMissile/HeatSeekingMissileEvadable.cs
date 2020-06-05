namespace O9K.Evader.Abilities.Heroes.Tinker.HeatSeekingMissile
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class HeatSeekingMissileEvadable : ProjectileEvadable
    {
        public HeatSeekingMissileEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.UnionWith(Abilities.VsProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.Counterspell);
        }
    }
}