namespace O9K.Evader.Abilities.Heroes.Tusk.IceShards
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tusk_ice_shards)]
    internal class IceShardsBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public IceShardsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IceShardsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}