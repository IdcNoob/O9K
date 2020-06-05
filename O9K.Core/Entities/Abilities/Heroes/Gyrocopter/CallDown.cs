namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.gyrocopter_call_down)]
    public class CallDown : CircleAbility
    {
        public CallDown(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix cast range

            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "slow_duration_first");
            this.DamageData = new SpecialData(baseAbility, "damage_first");
        }

        public override float CastRange
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_gyrocopter_5);
                if (talent?.Level > 0)
                {
                    return 9999999;
                }

                return base.CastRange;
            }
        }
    }
}