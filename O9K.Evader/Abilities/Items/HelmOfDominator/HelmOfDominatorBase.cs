namespace O9K.Evader.Abilities.Items.HelmOfDominator
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    using HandOfMidas;

    [AbilityId(AbilityId.item_helm_of_the_dominator)]
    internal class HelmOfDominatorBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public HelmOfDominatorBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new HandOfMidasUsable(this.Ability, this.Menu);
        }
    }
}