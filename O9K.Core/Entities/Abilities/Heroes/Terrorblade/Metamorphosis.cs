namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.terrorblade_metamorphosis)]
    public class Metamorphosis : ActiveAbility, IHasRangeIncrease, IBuff
    {
        private readonly SpecialData attackRange;

        public Metamorphosis(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "bonus_range");
        }

        public string BuffModifierName { get; } = "modifier_terrorblade_metamorphosis";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_terrorblade_metamorphosis";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }
    }
}