namespace O9K.Evader.Abilities.Items.Tango
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_tango)]
    [AbilityId(AbilityId.item_tango_single)]
    internal class TangoBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public TangoBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new TangoUsable(this.Ability, this.Menu);
        }
    }
}