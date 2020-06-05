namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.gyrocopter_rocket_barrage)]
    public class RocketBarrage : AreaOfEffectAbility
    {
        public RocketBarrage(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "rocket_damage");
        }
    }
}