namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.troll_warlord_berserkers_rage)]
    public class BerserkersRage : ToggleAbility, IHasRangeIncrease, ISpeedBuff
    {
        private readonly SpecialData attackRange;

        private readonly SpecialData bonusMoveSpeedData;

        public BerserkersRage(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "bonus_range");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_move_speed");
        }

        public string BuffModifierName { get; } = "modifier_troll_warlord_berserkers_rage";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_troll_warlord_berserkers_rage";

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return -this.attackRange.GetValue(this.Level);
        }

        public float GetSpeedBuff(Unit9 unit)
        {
            return this.bonusMoveSpeedData.GetValue(this.Level);
        }
    }
}