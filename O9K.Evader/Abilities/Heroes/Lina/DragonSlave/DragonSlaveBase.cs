namespace O9K.Evader.Abilities.Heroes.Lina.DragonSlave
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lina_dragon_slave)]
    internal class DragonSlaveBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public DragonSlaveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DragonSlaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}