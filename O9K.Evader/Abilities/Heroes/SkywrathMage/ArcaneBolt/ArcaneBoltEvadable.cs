namespace O9K.Evader.Abilities.Heroes.SkywrathMage.ArcaneBolt
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ArcaneBoltEvadable : ProjectileEvadable
    {
        public ArcaneBoltEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.GlimmerCape);
        }

        public override bool IsDisjointable { get; } = false;
    }
}