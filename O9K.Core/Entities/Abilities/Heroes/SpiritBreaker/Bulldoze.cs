namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spirit_breaker_bulldoze)]
    public class Bulldoze : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public Bulldoze(Ability baseAbility)
            : base(baseAbility)
        {
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "movement_speed");
        }

        public string BuffModifierName { get; } = "modifier_spirit_breaker_bulldoze";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}