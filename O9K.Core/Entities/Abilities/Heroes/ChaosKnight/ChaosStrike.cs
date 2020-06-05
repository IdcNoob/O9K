namespace O9K.Core.Entities.Abilities.Heroes.ChaosKnight
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.chaos_knight_chaos_strike)]
    public class ChaosStrike : PassiveAbility, IHasPassiveDamageIncrease
    {
        public ChaosStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "crit_min");
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!unit.IsBuilding && !unit.IsAlly(this.Owner) && this.CanBeCasted())
            {
                var crit = this.DamageData.GetValue(this.Level) / 100;
                var simpleAutoAttackDamage = unit.BaseUnit.MinimumDamage + unit.BaseUnit.BonusDamage;
                damage[this.DamageType] = simpleAutoAttackDamage * crit;
            }

            return damage;
        }
    }
}