namespace O9K.Evader.Abilities.Heroes.Ursa.Earthshock
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ursa_earthshock)]
    internal class EarthshockBase : EvaderBaseAbility, IEvadable
    {
        public EarthshockBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EarthshockEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}