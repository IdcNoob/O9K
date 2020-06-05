namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.antimage_blink)]
    public class Blink : RangedAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        public Blink(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRangeData = new SpecialData(baseAbility, "blink_range");
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