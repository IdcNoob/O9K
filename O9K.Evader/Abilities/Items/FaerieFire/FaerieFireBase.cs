namespace O9K.Evader.Abilities.Items.FaerieFire
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_faerie_fire)]
    [AbilityId(AbilityId.item_greater_faerie_fire)]
    internal class FaerieFireBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public FaerieFireBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}