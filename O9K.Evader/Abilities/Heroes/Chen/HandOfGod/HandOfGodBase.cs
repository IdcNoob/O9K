namespace O9K.Evader.Abilities.Heroes.Chen.HandOfGod
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chen_hand_of_god)]
    internal class HandOfGodBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public HandOfGodBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HandOfGodEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}