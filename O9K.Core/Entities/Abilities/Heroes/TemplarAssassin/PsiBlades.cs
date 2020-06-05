namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_psi_blades)]
    public class PsiBlades : PassiveAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        private readonly SpecialData splitRange;

        public PsiBlades(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
            this.splitRange = new SpecialData(baseAbility, "attack_spill_range");
        }

        public override float CastRange
        {
            get
            {
                return this.Owner.GetAttackRange() + this.SplitRange;
            }
        }

        public bool IsRangeIncreasePermanent { get; } = true;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_templar_assassin_psi_blades";

        public float SplitRange
        {
            get
            {
                return this.splitRange.GetValue(this.Level);
            }
        }

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}