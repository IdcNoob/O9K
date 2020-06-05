namespace O9K.Evader.Abilities.Heroes.TreantProtector.Overgrowth
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.treant_overgrowth)]
    internal class OvergrowthBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public OvergrowthBase(Ability9 ability)
            : base(ability)
        {
            //todo aghs trees
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OvergrowthEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}