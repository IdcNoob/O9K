namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.winter_wyvern_arctic_burn)]
    public class ArcticBurn : ActiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public ArcticBurn(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "attack_range_bonus");
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_winter_wyvern_arctic_burn_flight";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}