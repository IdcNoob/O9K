namespace O9K.Core.Entities.Abilities.Heroes.DrowRanger
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.drow_ranger_frost_arrows)]
    public class FrostArrows : OrbAbility, IHasPassiveDamageIncrease, IHarass
    {
        public FrostArrows(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public override string OrbModifier { get; } = "modifier_drow_ranger_frost_arrows_slow";

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
            if (unit.IsAlly(this.Owner) || unit.IsBuilding)
            {
                return 0;
            }

            return this.DamageData.GetValue(this.Level);
        }
    }
}