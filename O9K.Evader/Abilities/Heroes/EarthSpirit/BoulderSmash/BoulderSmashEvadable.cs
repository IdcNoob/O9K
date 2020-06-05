namespace O9K.Evader.Abilities.Heroes.EarthSpirit.BoulderSmash
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class BoulderSmashEvadable : LinearAreaOfEffectEvadable
    {
        public BoulderSmashEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}