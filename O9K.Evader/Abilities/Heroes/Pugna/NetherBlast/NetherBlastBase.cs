namespace O9K.Evader.Abilities.Heroes.Pugna.NetherBlast
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pugna_nether_blast)]
    internal class NetherBlastBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public NetherBlastBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new NetherBlastEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}