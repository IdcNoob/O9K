namespace O9K.Evader.Abilities.Units.Courier.Shield
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.courier_shield)]
    internal class ShieldBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public ShieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new ShieldUsable(this.Ability, this.Menu);
        }
    }
}