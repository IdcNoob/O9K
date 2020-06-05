namespace O9K.Core.Entities.Abilities.Talents
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.special_bonus_cast_range_50)]
    [AbilityId(AbilityId.special_bonus_cast_range_60)]
    [AbilityId(AbilityId.special_bonus_cast_range_75)]
    [AbilityId(AbilityId.special_bonus_cast_range_100)]
    [AbilityId(AbilityId.special_bonus_cast_range_125)]
    [AbilityId(AbilityId.special_bonus_cast_range_150)]
    [AbilityId(AbilityId.special_bonus_cast_range_175)]
    [AbilityId(AbilityId.special_bonus_cast_range_200)]
    [AbilityId((AbilityId)443)] // 225
    [AbilityId(AbilityId.special_bonus_cast_range_250)]
    [AbilityId(AbilityId.special_bonus_cast_range_275)]
    [AbilityId(AbilityId.special_bonus_cast_range_300)]
    [AbilityId((AbilityId)430)] // 325
    [AbilityId(AbilityId.special_bonus_cast_range_350)]
    [AbilityId(AbilityId.special_bonus_cast_range_400)]
    public class CastRangeTalent : Talent, IHasRangeIncrease
    {
        private readonly SpecialData castRange;

        public CastRangeTalent(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRange = new SpecialData(baseAbility, "value");
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Ability;

        public string RangeModifierName { get; } = "modifier_special_bonus_cast_range";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.castRange.GetValue(this.Level);
        }
    }
}