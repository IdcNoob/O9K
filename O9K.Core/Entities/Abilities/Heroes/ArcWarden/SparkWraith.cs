namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.arc_warden_spark_wraith)]
    public class SparkWraith : CircleAbility, INuke
    {
        public SparkWraith(Ability baseAbility)
            : base(baseAbility)
        {
            //this.SpeedData = new SpecialData(baseAbility, "wraith_speed");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "spark_damage");
            this.ActivationDelayData = new SpecialData(baseAbility, "activation_delay");
        }
    }
}