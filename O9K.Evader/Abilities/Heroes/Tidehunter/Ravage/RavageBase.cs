namespace O9K.Evader.Abilities.Heroes.Tidehunter.Ravage
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tidehunter_ravage)]
    internal class RavageBase : EvaderBaseAbility, IEvadable
    {
        public RavageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RavageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}