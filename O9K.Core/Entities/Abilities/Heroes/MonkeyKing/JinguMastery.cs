namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.monkey_king_jingu_mastery)]
    public class JinguMastery : PassiveAbility, IHasPassiveDamageIncrease
    {
        public JinguMastery(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = true;

        public string PassiveDamageModifierName { get; } = "modifier_monkey_king_quadruple_tap_bonuses";

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.Owner.HasModifier(this.PassiveDamageModifierName) ? this.DamageData.GetValue(this.Level) : 0
            };
        }
    }
}