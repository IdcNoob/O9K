namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.clinkz_searing_arrows)]
    public class SearingArrows : OrbAbility, IHarass, IHasPassiveDamageIncrease
    {
        public SearingArrows(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_bonus");
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
            if (unit.IsAlly(this.Owner))
            {
                return 0;
            }

            return this.DamageData.GetValue(this.Level);
        }
    }
}