namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.earth_spirit_geomagnetic_grip)]
    public class GeomagneticGrip : LineAbility
    {
        public GeomagneticGrip(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "rock_damage");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }
    }
}