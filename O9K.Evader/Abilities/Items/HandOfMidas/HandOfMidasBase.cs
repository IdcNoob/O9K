namespace O9K.Evader.Abilities.Items.HandOfMidas
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_hand_of_midas)]
    internal class HandOfMidasBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public HandOfMidasBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new HandOfMidasUsable(this.Ability, this.Menu);
        }
    }
}