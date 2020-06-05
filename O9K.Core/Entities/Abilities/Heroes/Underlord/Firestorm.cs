namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.abyssal_underlord_firestorm)]
    public class Firestorm : CircleAbility, IHarass
    {
        public Firestorm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}