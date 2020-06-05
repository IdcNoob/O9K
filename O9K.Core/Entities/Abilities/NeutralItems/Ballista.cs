namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.item_ballista)]
    public class Ballista : PassiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public Ballista(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "attack_range_bonus");
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_item_dragon_lance";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.IsUsable || !this.Owner.IsRanged)
            {
                return 0;
            }

            return this.attackRange.GetValue(this.Level);
        }
    }
}