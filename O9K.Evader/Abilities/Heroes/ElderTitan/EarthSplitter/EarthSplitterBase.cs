namespace O9K.Evader.Abilities.Heroes.ElderTitan.EarthSplitter
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.elder_titan_earth_splitter)]
    internal class EarthSplitterBase : EvaderBaseAbility, IEvadable
    {
        public EarthSplitterBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EarthSplitterEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}