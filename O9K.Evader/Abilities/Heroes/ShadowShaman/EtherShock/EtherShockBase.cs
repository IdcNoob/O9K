namespace O9K.Evader.Abilities.Heroes.ShadowShaman.EtherShock
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shadow_shaman_ether_shock)]
    internal class EtherShockBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public EtherShockBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EtherShockEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}