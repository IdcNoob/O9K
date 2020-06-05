namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.mirana_leap)]
    public class Leap : ActiveAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        public Leap(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "leap_speed");
            this.castRangeData = new SpecialData(baseAbility, "leap_distance");
        }

        public BlinkType BlinkType { get; } = BlinkType.Leap;

        public override float CastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override float GetHitTime(Vector3 position)
        {
            return this.GetCastDelay(position) + this.ActivationDelay + (this.Range / this.Speed);
        }
    }
}