namespace O9K.Evader.Abilities.Items.OrchidMalevolence
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_orchid)]
    internal class OrchidMalevolenceBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public OrchidMalevolenceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OrchidMalevolenceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}