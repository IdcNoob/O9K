namespace O9K.Evader.Abilities.Heroes.Zeus.Nimbus
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class NimbusEvadable : AreaOfEffectEvadable
    {
        public NimbusEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}