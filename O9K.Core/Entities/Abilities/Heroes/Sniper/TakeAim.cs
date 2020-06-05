namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.sniper_take_aim)]
    public class TakeAim : ActiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public TakeAim(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_sniper_take_aim";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}