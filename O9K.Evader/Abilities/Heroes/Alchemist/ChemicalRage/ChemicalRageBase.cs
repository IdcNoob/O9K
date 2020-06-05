namespace O9K.Evader.Abilities.Heroes.Alchemist.ChemicalRage
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.alchemist_chemical_rage)]
    internal class ChemicalRageBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public ChemicalRageBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChemicalRageEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}