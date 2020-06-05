namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.faceless_void_time_walk)]
    public class TimeWalk : RangedAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        public TimeWalk(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.castRangeData = new SpecialData(baseAbility, "range");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}