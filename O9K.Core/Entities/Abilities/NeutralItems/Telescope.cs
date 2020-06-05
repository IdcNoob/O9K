namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.item_spy_gadget)]
    public class Telescope : PassiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        private readonly SpecialData castRange;

        public Telescope(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRange = new SpecialData(baseAbility, "cast_range");
            this.attackRange = new SpecialData(baseAbility, "attack_range");
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Ability | RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_item_spy_gadget";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.IsUsable)
            {
                return 0;
            }

            if (type == RangeIncreaseType.Attack)
            {
                if (!unit.IsRanged)
                {
                    return 0;
                }

                return this.attackRange.GetValue(this.Level);
            }

            return this.castRange.GetValue(this.Level);
        }
    }
}