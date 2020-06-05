namespace O9K.Core.Entities.Abilities.Heroes.Snapfire
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId((AbilityId)6486)]
    public class SpitOut : CircleAbility, INuke, IDisable
    {
        public SpitOut(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.RadiusData = new SpecialData(baseAbility, "impact_radius");
            this.DamageData = new SpecialData(baseAbility, "impact_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}