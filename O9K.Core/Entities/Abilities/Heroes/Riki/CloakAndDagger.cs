namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.riki_backstab)]
    public class CloakAndDagger : PassiveAbility, IHasPassiveDamageIncrease
    {
        public CloakAndDagger(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_multiplier");
        }

        public override bool IntelligenceAmplify { get; } = false;

        public override bool IsInvisibility { get; } = true;

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override int GetDamage(Unit9 unit)
        {
            if (unit.GetAngle(this.Owner.Position) < 1.5)
            {
                return 0;
            }

            return base.GetDamage(unit);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);

            damage[this.DamageType] *= this.Owner.TotalAgility;

            return damage;
        }

        Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && !unit.IsAlly(this.Owner) && !this.Owner.IsIllusion && unit.GetAngle(this.Owner) > 2)
            {
                damage[this.DamageType] = this.DamageData.GetValue(this.Level) * this.Owner.TotalAgility;
            }

            return damage;
        }
    }
}