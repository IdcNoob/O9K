namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.ancient_apparition_chilling_touch)]
    public class ChillingTouch : OrbAbility, IHarass, IHasPassiveDamageIncrease
    {
        private readonly SpecialData attackRangeIncrease;

        public ChillingTouch(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.attackRangeIncrease = new SpecialData(baseAbility, "attack_range_bonus");
        }

        public override float CastRange
        {
            get
            {
                return this.Owner.GetAttackRange() + this.attackRangeIncrease.GetValue(this.Level);
            }
        }

        public override DamageType DamageType { get; } = DamageType.Magical; //todo delete?

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

            return this.DamageData.GetValue(this.Level);
        }
    }
}