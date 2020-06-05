namespace O9K.Evader.Abilities.Heroes.Beastmaster.WildAxes
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class WildAxesEvadable : LinearProjectileEvadable
    {
        public WildAxesEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo improve radius evade time calcs

            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.PhysShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
        }
    }
}