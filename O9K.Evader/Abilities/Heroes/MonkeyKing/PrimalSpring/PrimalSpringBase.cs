namespace O9K.Evader.Abilities.Heroes.MonkeyKing.PrimalSpring
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.monkey_king_primal_spring)]
    internal class PrimalSpringBase : EvaderBaseAbility //, IEvadable
    {
        public PrimalSpringBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PrimalSpringEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}