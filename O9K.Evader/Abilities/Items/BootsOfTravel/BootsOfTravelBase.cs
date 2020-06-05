namespace O9K.Evader.Abilities.Items.BootsOfTravel
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_travel_boots)]
    internal class BootsOfTravelBase : EvaderBaseAbility, IEvadable
    {
        public BootsOfTravelBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BootsOfTravelEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}