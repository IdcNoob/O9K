namespace O9K.Evader.Abilities.Heroes.Visage.SoulAssumption
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.visage_soul_assumption)]
    internal class SoulAssumptionBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public SoulAssumptionBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SoulAssumptionEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}