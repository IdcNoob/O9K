namespace O9K.Evader.Abilities.Heroes.Clockwerk.PowerCogs
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class PowerCogsEvadable : AreaOfEffectEvadable
    {
        public PowerCogsEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}