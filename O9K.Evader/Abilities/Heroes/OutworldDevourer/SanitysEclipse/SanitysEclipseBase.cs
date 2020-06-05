namespace O9K.Evader.Abilities.Heroes.OutworldDevourer.SanitysEclipse
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.obsidian_destroyer_sanity_eclipse)]
    internal class SanitysEclipseBase : EvaderBaseAbility, IEvadable
    {
        public SanitysEclipseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SanitysEclipseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}