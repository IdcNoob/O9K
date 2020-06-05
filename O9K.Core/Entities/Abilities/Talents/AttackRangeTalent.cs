namespace O9K.Core.Entities.Abilities.Talents
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.special_bonus_attack_range_50)]
    [AbilityId(AbilityId.special_bonus_attack_range_75)]
    [AbilityId(AbilityId.special_bonus_attack_range_100)]
    [AbilityId(AbilityId.special_bonus_attack_range_125)]
    [AbilityId(AbilityId.special_bonus_attack_range_150)]
    [AbilityId(AbilityId.special_bonus_attack_range_175)]
    [AbilityId(AbilityId.special_bonus_attack_range_200)]
    [AbilityId(AbilityId.special_bonus_attack_range_250)]
    [AbilityId((AbilityId)481)] // 275
    [AbilityId(AbilityId.special_bonus_attack_range_300)]
    [AbilityId(AbilityId.special_bonus_attack_range_400)]
    public class AttackRangeTalent : Talent, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public AttackRangeTalent(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "value");
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_special_bonus_attack_range";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.Owner.IsRanged)
            {
                return 0;
            }

            return this.attackRange.GetValue(this.Level);
        }
    }
}