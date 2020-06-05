namespace O9K.Core.Entities.Abilities.Heroes.Disruptor
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.disruptor_thunder_strike)]
    public class ThunderStrike : RangedAbility, IHarass
    {
        public ThunderStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "strike_damage");
        }
    }
}