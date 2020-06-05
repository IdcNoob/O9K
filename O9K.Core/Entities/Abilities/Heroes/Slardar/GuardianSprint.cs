namespace O9K.Core.Entities.Abilities.Heroes.Slardar
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.slardar_sprint)]
    public class GuardianSprint : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public GuardianSprint(Ability baseAbility)
            : base(baseAbility)
        {
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_speed");
        }

        public string BuffModifierName { get; } = "modifier_slardar_sprint";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}