namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.silencer_glaives_of_wisdom)]
    public class GlaivesOfWisdom : OrbAbility, IHarass, IHasPassiveDamageIncrease
    {
        public GlaivesOfWisdom(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "intellect_damage_pct");
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

            return (this.Owner.TotalIntelligence * this.DamageData.GetValue(this.Level)) / 100;
        }
    }
}