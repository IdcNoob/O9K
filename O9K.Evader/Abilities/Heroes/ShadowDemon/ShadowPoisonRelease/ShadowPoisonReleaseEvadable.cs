namespace O9K.Evader.Abilities.Heroes.ShadowDemon.ShadowPoisonRelease
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal class ShadowPoisonReleaseEvadable : TargetableEvadable
    {
        public ShadowPoisonReleaseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }
    }
}