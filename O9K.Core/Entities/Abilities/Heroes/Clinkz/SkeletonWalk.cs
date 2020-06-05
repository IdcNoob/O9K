namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.clinkz_wind_walk)]
    public class SkeletonWalk : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public SkeletonWalk(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "fade_time");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "move_speed_bonus_pct");
        }

        public string BuffModifierName { get; } = "modifier_clinkz_wind_walk";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public override bool IsInvisibility { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}