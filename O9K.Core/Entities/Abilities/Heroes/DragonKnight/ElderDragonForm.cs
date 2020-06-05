namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.dragon_knight_elder_dragon_form)]
    public class ElderDragonForm : ActiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public ElderDragonForm(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_dragon_knight_dragon_form";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}