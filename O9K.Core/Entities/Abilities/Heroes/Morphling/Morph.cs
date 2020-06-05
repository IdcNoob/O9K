namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.morphling_replicate)]
    public class Morph : RangedAbility, IHasRangeIncrease
    {
        private readonly SpecialData castRangeIncrease;

        public Morph(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRangeIncrease = new SpecialData(baseAbility, "scepter_cast_range_bonus");
        }

        public bool CanTargetAlly
        {
            get
            {
                //todo change
                return this.Owner.GetAbilityById(AbilityId.special_bonus_unique_morphling_5)?.Level > 0;
            }
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Ability;

        public string RangeModifierName { get; } = "modifier_morphling_scepter";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.castRangeIncrease.GetValue(this.Level);
        }
    }
}