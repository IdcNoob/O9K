namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.gyrocopter_homing_missile)]
    public class HomingMissile : RangedAbility
    {
        private readonly SpecialData speedAccelerationData;

        private bool talentLearned;

        public HomingMissile(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "pre_flight_time");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.speedAccelerationData = new SpecialData(baseAbility, "acceleration");
        }

        public float Acceleration
        {
            get
            {
                return this.speedAccelerationData.GetValue(this.Level);
            }
        }

        public override bool IsDisplayingCharges
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_gyrocopter_1)?.Level > 0;
            }
        }
    }
}