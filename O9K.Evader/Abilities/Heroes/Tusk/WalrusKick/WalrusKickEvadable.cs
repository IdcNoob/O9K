namespace O9K.Evader.Abilities.Heroes.Tusk.WalrusKick
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class WalrusKickEvadable : TargetableEvadable
    {
        public WalrusKickEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.LinkensSphere);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.LotusOrb);
        }
    }
}