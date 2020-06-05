namespace O9K.Evader.Abilities.Heroes.Enchantress.NaturesAttendants
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.enchantress_natures_attendants)]
    internal class NaturesAttendantsBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public NaturesAttendantsBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}