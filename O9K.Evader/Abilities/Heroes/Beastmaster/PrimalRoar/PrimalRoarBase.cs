namespace O9K.Evader.Abilities.Heroes.Beastmaster.PrimalRoar
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.beastmaster_primal_roar)]
    internal class PrimalRoarBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public PrimalRoarBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PrimalRoarEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}