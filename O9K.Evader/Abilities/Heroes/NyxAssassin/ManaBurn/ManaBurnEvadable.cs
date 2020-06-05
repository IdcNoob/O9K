namespace O9K.Evader.Abilities.Heroes.NyxAssassin.ManaBurn
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class ManaBurnEvadable : TargetableEvadable
    {
        public ManaBurnEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.LotusOrb);
        }

        protected override void AddObstacle()
        {
            if (this.Owner.HasModifier("modifier_nyx_assassin_burrow"))
            {
                return;
            }

            base.AddObstacle();
        }
    }
}