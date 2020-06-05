namespace O9K.Evader.Abilities.Units.Courier.SpeedBurst
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.courier_burst)]
    internal class SpeedBurstBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public SpeedBurstBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new SpeedBurstUsable(this.Ability, this.Menu);
        }
    }
}