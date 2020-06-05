namespace O9K.Evader.Abilities.Items.IronBranch
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_branches)]
    [AbilityId(AbilityId.item_ironwood_tree)]
    internal class IronBranchBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public IronBranchBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new IronBranchUsable(this.Ability, this.Menu);
        }
    }
}