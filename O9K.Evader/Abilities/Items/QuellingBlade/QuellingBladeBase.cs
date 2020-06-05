namespace O9K.Evader.Abilities.Items.QuellingBlade
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_quelling_blade)]
    [AbilityId(AbilityId.item_bfury)]
    [AbilityId(AbilityId.item_iron_talon)]
    internal class QuellingBladeBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public QuellingBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new QuellingBladeUsable(this.Ability, this.Menu);
        }
    }
}