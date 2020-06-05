namespace O9K.Evader.Abilities.Heroes.Rubick.SpellSteal
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SpellStealEvadable : TargetableEvadable
    {
        public SpellStealEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Counterspell);
        }
    }
}