namespace O9K.Core.Entities.Abilities.Items
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_blink)]
    public class BlinkDagger : RangedAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        public BlinkDagger(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRangeData = new SpecialData(baseAbility, "blink_range");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override float TimeSinceCasted
        {
            get
            {
                //prevent damage 3s cd to consider as casted
                var cooldownLength = this.BaseAbility.CooldownLength;
                return cooldownLength <= 0 ? 9999999 : Math.Max(cooldownLength, 10) - this.BaseAbility.Cooldown;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level) - 25;
            }
        }
    }
}