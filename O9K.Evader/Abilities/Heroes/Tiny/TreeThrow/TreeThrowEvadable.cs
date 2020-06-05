namespace O9K.Evader.Abilities.Heroes.Tiny.TreeThrow
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class TreeThrowEvadable : LinearProjectileEvadable
    {
        public TreeThrowEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add projectile obstacle

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.SimplePhysShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
        }
    }
}