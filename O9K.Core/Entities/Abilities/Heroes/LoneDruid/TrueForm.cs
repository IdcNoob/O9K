namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_true_form)]
    public class TrueForm : ActiveAbility, IHasRangeIncrease
    {
        public TrueForm(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_lone_druid_true_form";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return -400; // no special data
        }
    }
}