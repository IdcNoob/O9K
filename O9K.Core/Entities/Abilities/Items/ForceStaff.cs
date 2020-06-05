namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.item_force_staff)]
    public class ForceStaff : RangedAbility, IBlink
    {
        public ForceStaff(Ability baseAbility)
            : base(baseAbility)
        {
            this.RangeData = new SpecialData(baseAbility, "push_length");
        }

        public BlinkType BlinkType { get; } = BlinkType.Leap;

        public override float Range
        {
            get
            {
                return this.RangeData.GetValue(this.Level);
            }
        }

        public override float Speed { get; } = 1200;

        public override float GetHitTime(Vector3 position)
        {
            return this.GetCastDelay(position) + this.ActivationDelay + (this.Range / this.Speed);
        }
    }
}