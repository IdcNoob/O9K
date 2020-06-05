namespace O9K.Evader.Abilities.Heroes.PhantomAssassin.StiflingDagger
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phantom_assassin_stifling_dagger)]
    internal class StiflingDaggerBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public StiflingDaggerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StiflingDaggerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}