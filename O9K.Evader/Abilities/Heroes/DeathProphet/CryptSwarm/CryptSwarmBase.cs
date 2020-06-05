namespace O9K.Evader.Abilities.Heroes.DeathProphet.CryptSwarm
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.death_prophet_carrion_swarm)]
    internal class CryptSwarmBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public CryptSwarmBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CryptSwarmEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}