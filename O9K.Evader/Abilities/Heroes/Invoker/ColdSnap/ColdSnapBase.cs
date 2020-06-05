namespace O9K.Evader.Abilities.Heroes.Invoker.ColdSnap
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_cold_snap)]
    internal class ColdSnapBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>, IUsable<CounterEnemyAbility>
    {
        public ColdSnapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ColdSnapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }

        CounterEnemyAbility IUsable<CounterEnemyAbility>.GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}