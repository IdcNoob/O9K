namespace O9K.Evader.Abilities.Heroes.Earthshaker.Aftershock
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.earthshaker_aftershock)]
    internal class AftershockBase : EvaderBaseAbility, IEvadable
    {
        public AftershockBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AftershockEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}