namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_storm_dispel_magic)]
    public class DispelMagic : CircleAbility
    {
        public DispelMagic(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}