namespace O9K.Evader.Abilities.Heroes.Warlock.FatalBonds
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.warlock_fatal_bonds)]
    internal class FatalBondsBase : EvaderBaseAbility, IEvadable
    {
        public FatalBondsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FatalBondsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}