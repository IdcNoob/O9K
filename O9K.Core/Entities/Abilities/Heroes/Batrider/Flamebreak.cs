namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.batrider_flamebreak)]
    public class Flamebreak : CircleAbility
    {
        public Flamebreak(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "explosion_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage_impact");
        }
    }
}