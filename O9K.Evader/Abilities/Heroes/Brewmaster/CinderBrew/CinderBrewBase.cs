namespace O9K.Evader.Abilities.Heroes.Brewmaster.CinderBrew
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.brewmaster_cinder_brew)]
    internal class CinderBrewBase : EvaderBaseAbility, IEvadable
    {
        public CinderBrewBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CinderBrewEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}