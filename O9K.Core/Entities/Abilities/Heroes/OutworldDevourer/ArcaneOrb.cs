namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.obsidian_destroyer_arcane_orb)]
    public class ArcaneOrb : OrbAbility, IHarass, IHasPassiveDamageIncrease
    {
        public ArcaneOrb(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "mana_pool_damage_pct");
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

            return (this.Owner.Mana * this.DamageData.GetValue(this.Level)) / 100;
        }
    }
}