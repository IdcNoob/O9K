namespace O9K.Evader.Abilities.Items.Mekansm
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_mekansm)]
    internal class MekansmBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MekansmBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}