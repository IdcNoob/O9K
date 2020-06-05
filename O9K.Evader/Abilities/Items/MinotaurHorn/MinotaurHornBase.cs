namespace O9K.Evader.Abilities.Items.MinotaurHorn
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_minotaur_horn)]
    internal class MinotaurHornBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public MinotaurHornBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}