namespace O9K.Evader.Abilities.Heroes.Snapfire.FiresnapCookie
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.snapfire_firesnap_cookie)]
    internal class FiresnapCookieBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public FiresnapCookieBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FiresnapCookieEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new FiresnapCookieUsable(this.Ability, this.Menu);
        }
    }
}