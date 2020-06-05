namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.item_enchanted_quiver)]
    public class EnchantedQuiver : PassiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public EnchantedQuiver(Ability baseAbility)
            : base(baseAbility)
        {
            //todo add damage ?
            this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_item_enchanted_quiver";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            if (!this.IsReady || !this.Owner.IsRanged)
            {
                return 0;
            }

            return this.attackRange.GetValue(this.Level);
        }
    }
}