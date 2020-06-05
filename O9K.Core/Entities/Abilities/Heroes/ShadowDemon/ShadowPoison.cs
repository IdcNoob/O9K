namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shadow_demon_shadow_poison)]
    public class ShadowPoison : LineAbility, IHarass
    {
        public ShadowPoison(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }
    }
}