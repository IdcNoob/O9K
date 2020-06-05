namespace O9K.Evader.Abilities.Units.AncientProwlerShaman.Desecrate
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class DesecrateEvadable : AreaOfEffectEvadable
    {
        public DesecrateEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}