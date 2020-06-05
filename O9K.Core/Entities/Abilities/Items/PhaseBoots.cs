namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_phase_boots)]
    public class PhaseBoots : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        private readonly SpecialData bonusMoveSpeedRangedData;

        public PhaseBoots(Ability baseAbility)
            : base(baseAbility)
        {
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "phase_movement_speed");
            this.bonusMoveSpeedRangedData = new SpecialData(baseAbility, "phase_movement_speed_range");
        }

        public string BuffModifierName { get; } = "modifier_item_phase_boots_active";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * (unit.IsRanged
                                      ? this.bonusMoveSpeedRangedData.GetValue(this.Level)
                                      : this.bonusMoveSpeedData.GetValue(this.Level))) / 100;
        }
    }
}