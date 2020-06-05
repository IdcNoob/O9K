namespace O9K.Evader.Abilities.Heroes.Abaddon.BorrowedTime
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abaddon_borrowed_time)]
    internal class BorrowedTimeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BorrowedTimeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BorrowedTimeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo use when stunned ?
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}