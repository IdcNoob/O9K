namespace O9K.Evader.Abilities.Heroes.Pugna.NetherWard
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pugna_nether_ward)]
    internal class NetherWardBase : EvaderBaseAbility, IEvadable //, IUsable<CounterAbility>
    {
        public NetherWardBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new NetherWardEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}