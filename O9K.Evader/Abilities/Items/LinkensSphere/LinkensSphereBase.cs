namespace O9K.Evader.Abilities.Items.LinkensSphere
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_sphere)]
    internal class LinkensSphereBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public LinkensSphereBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new LinkensSphereUsable(this.Ability, this.Menu);
        }
    }
}