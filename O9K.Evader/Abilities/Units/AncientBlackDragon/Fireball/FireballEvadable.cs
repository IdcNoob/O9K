namespace O9K.Evader.Abilities.Units.AncientBlackDragon.Fireball
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class FireballEvadable : LinearAreaOfEffectEvadable
    {
        public FireballEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}