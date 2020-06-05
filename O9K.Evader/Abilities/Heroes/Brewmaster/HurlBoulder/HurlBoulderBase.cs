namespace O9K.Evader.Abilities.Heroes.Brewmaster.HurlBoulder
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.brewmaster_earth_hurl_boulder)]
    internal class HurlBoulderBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public HurlBoulderBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HurlBoulderEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}