namespace O9K.Evader.Abilities.Items.MedallionOfCourage
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_medallion_of_courage)]
    internal class MedallionOfCourageBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public MedallionOfCourageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MedallionOfCourageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}