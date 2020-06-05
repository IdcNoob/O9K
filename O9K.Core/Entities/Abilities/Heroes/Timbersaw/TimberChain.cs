namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shredder_timber_chain)]
    public class TimberChain : LineAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        private readonly SpecialData chainRadiusData;

        public TimberChain(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.chainRadiusData = new SpecialData(baseAbility, "chain_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.castRangeData = new SpecialData(baseAbility, "range");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public float ChainRadius
        {
            get
            {
                return this.chainRadiusData.GetValue(this.Level);
            }
        }

        public override bool HasAreaOfEffect { get; } = false;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}