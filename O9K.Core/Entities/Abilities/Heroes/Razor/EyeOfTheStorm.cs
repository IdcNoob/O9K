namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.razor_eye_of_the_storm)]
    public class EyeOfTheStorm : AreaOfEffectAbility
    {
        public EyeOfTheStorm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}