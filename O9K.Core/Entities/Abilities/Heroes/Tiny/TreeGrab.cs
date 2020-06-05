namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.tiny_tree_grab)]
    public class TreeGrab : RangedAbility, IHasRangeIncrease
    {
        private readonly SpecialData attackRange;

        public TreeGrab(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "splash_width");
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_tiny_craggy_exterior";

        public override bool TargetsEnemy { get; } = false;

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}