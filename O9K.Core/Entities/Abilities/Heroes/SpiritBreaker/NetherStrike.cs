namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spirit_breaker_nether_strike)]
    public class NetherStrike : RangedAbility
    {
        public NetherStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}