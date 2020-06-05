namespace O9K.Evader.Abilities.Heroes.SpiritBreaker.NetherStrike
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spirit_breaker_nether_strike)]
    internal class NetherStrikeBase : EvaderBaseAbility, IEvadable
    {
        public NetherStrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new NetherStrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}