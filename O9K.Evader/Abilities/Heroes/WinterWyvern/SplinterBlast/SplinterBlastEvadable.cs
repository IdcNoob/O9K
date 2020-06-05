namespace O9K.Evader.Abilities.Heroes.WinterWyvern.SplinterBlast
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SplinterBlastEvadable : ProjectileEvadable
    {
        public SplinterBlastEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool IsDisjointable { get; } = false;
    }
}