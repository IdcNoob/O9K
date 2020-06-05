namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
    using System;

    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.enchantress_impetus)]
    public class Impetus : OrbAbility, IHarass, IHasPassiveDamageIncrease
    {
        private readonly SpecialData damageCapRange;

        public Impetus(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "distance_damage_pct");
            this.damageCapRange = new SpecialData(baseAbility, "distance_cap");
        }

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!this.Enabled)
            {
                damage[this.DamageType] = this.GetOrbDamage(unit);
            }

            return damage + this.Owner.GetRawAttackDamage(unit);
        }

        Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
        {
            var damage = new Damage();

            if (this.Enabled)
            {
                damage[this.DamageType] = this.GetOrbDamage(unit);
            }

            return damage;
        }

        private float GetOrbDamage(Unit9 unit)
        {
            if (unit.IsBuilding || unit.IsAlly(this.Owner))
            {
                return 0;
            }

            return (this.DamageData.GetValue(this.Level) / 100) * Math.Min(
                       this.Owner.Distance(unit),
                       this.damageCapRange.GetValue(this.Level));
        }
    }
}