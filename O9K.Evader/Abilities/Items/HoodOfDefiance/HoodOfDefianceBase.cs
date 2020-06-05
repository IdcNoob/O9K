namespace O9K.Evader.Abilities.Items.HoodOfDefiance
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_hood_of_defiance)]
    internal class HoodOfDefianceBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public HoodOfDefianceBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}