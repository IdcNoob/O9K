namespace O9K.Evader.Abilities.Items.Cheese
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_cheese)]
    internal class CheeseBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public CheeseBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}