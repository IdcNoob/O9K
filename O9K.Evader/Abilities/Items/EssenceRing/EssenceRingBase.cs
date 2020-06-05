namespace O9K.Evader.Abilities.Items.EssenceRing
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_essence_ring)]
    internal class EssenceRingBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public EssenceRingBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}