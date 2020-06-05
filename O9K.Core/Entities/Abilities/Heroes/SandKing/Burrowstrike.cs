namespace O9K.Core.Entities.Abilities.Heroes.SandKing
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.sandking_burrowstrike)]
    public class Burrowstrike : LineAbility, IDisable, INuke, IBlink
    {
        private readonly SpecialData scepterCastRangeData;

        private readonly SpecialData scepterSpeedData;

        public Burrowstrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "burrow_width");
            this.SpeedData = new SpecialData(baseAbility, "burrow_speed");
            this.scepterSpeedData = new SpecialData(baseAbility, "burrow_speed_scepter");
            this.scepterCastRangeData = new SpecialData(baseAbility, "cast_range_scepter");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override float Speed
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterSpeedData.GetValue(this.Level);
                }

                return base.Speed;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterCastRangeData.GetValue(this.Level);
                }

                return base.BaseCastRange;
            }
        }
    }
}