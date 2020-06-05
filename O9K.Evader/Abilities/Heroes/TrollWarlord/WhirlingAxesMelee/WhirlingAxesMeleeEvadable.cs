namespace O9K.Evader.Abilities.Heroes.TrollWarlord.WhirlingAxesMelee
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class WhirlingAxesMeleeEvadable : AreaOfEffectEvadable
    {
        public WhirlingAxesMeleeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}