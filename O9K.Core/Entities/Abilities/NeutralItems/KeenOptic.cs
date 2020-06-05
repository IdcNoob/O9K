namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.item_keen_optic)]
    public class KeenOptic : PassiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData castRange;

        public KeenOptic(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRange = new SpecialData(baseAbility, "cast_range_bonus");
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Ability;

        public string RangeModifierName { get; } = "modifier_item_keen_optic";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.IsUsable)
            {
                return 0;
            }

            return this.castRange.GetValue(this.Level);
        }
    }
}