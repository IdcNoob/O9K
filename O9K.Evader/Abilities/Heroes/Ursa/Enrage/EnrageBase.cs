namespace O9K.Evader.Abilities.Heroes.Ursa.Enrage
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ursa_enrage)]
    internal class EnrageBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public EnrageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EnrageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new EnrageUsable(this.Ability, this.Menu);
        }
    }
}