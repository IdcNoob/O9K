namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.viper_viper_strike)]
    public class ViperStrike : RangedAbility, IDebuff
    {
        private readonly SpecialData castRangeData;

        public ViperStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.castRangeData = new SpecialData(baseAbility, "cast_range_scepter");
        }

        public string DebuffModifierName { get; } = "modifier_viper_viper_strike_slow";

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.castRangeData.GetValue(this.Level);
                }

                return base.BaseCastRange;
            }
        }
    }
}