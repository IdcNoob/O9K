namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.weaver_shukuchi)]
    public class Shukuchi : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData speedBuffData;

        public Shukuchi(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.speedBuffData = new SpecialData(baseAbility, "speed");
            this.ActivationDelayData = new SpecialData(baseAbility, "fade_time");
        }

        public string BuffModifierName { get; } = "modifier_weaver_shukuchi";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public override bool IsInvisibility { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return this.speedBuffData.GetValue(this.Level);
        }
    }
}