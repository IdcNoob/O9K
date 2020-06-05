namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.batrider_firefly)]
    public class Firefly : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public Firefly(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage_per_second");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "movement_speed");
        }

        public string BuffModifierName { get; } = "modifier_batrider_firefly";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return unit.Speed * (this.bonusMoveSpeedData.GetValue(this.Level) / 100f);
        }
    }
}