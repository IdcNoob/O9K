namespace O9K.Evader.Abilities.Heroes.Oracle.PurifyingFlames
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.oracle_purifying_flames)]
    internal class PurifyingFlamesBase : EvaderBaseAbility, IUsable<CounterAbility>, IEvadable
    {
        public PurifyingFlamesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PurifyingFlamesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}