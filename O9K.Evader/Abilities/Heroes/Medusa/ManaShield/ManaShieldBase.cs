namespace O9K.Evader.Abilities.Heroes.Medusa.ManaShield
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.medusa_mana_shield)]
    internal class ManaShieldBase : EvaderBaseAbility //, IUsable<CounterAbility>
    {
        public ManaShieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}