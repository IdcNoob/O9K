namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bloodseeker_blood_bath)]
    public class BloodRite : CircleAbility, IDisable
    {
        public BloodRite(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "delay");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;
    }
}