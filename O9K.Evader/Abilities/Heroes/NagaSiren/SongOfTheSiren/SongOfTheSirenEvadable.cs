namespace O9K.Evader.Abilities.Heroes.NagaSiren.SongOfTheSiren
{
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Metadata;

    internal sealed class SongOfTheSirenEvadable : AreaOfEffectEvadable
    {
        public SongOfTheSirenEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.Add(Abilities.GlobalSilence);
            this.Disables.UnionWith(Abilities.Disable);
        }
    }
}