namespace O9K.Evader.Abilities.Heroes.Invoker.Alacrity
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_alacrity)]
    internal class AlacrityBase : EvaderBaseAbility, IEvadable
    {
        public AlacrityBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AlacrityEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}