namespace O9K.Evader.Abilities.Heroes.SpiritBreaker.ChargeOfDarkness
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spirit_breaker_charge_of_darkness)]
    internal class ChargeOfDarknessBase : EvaderBaseAbility, IEvadable
    {
        public ChargeOfDarknessBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChargeOfDarknessEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}