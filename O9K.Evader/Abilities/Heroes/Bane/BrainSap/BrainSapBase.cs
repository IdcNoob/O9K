namespace O9K.Evader.Abilities.Heroes.Bane.BrainSap
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bane_brain_sap)]
    internal class BrainSapBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public BrainSapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BrainSapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}