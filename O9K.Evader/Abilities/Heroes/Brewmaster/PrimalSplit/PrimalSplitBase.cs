namespace O9K.Evader.Abilities.Heroes.Brewmaster.PrimalSplit
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.brewmaster_primal_split)]
    internal class PrimalSplitBase : EvaderBaseAbility, IEvadable
    {
        public PrimalSplitBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PrimalSplitEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}