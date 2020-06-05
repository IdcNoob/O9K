namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.gyrocopter_flak_cannon)]
    public class FlakCannon : AreaOfEffectAbility
    {
        public FlakCannon(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}