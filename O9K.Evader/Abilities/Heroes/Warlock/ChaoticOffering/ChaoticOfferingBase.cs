namespace O9K.Evader.Abilities.Heroes.Warlock.ChaoticOffering
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.warlock_rain_of_chaos)]
    internal class ChaoticOfferingBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ChaoticOfferingBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChaoticOfferingEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}