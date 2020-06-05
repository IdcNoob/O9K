namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.kunkka_tidebringer)]
    public class Tidebringer : AutoCastAbility, IHasPassiveDamageIncrease
    {
        private readonly SpecialData endRadiusData;

        public Tidebringer(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "cleave_starting_width");
            this.endRadiusData = new SpecialData(baseAbility, "cleave_ending_width");
            this.RangeData = new SpecialData(baseAbility, "cleave_distance");
            this.DamageData = new SpecialData(baseAbility, "damage_bonus");
            //todo add cleave dmg talent ?
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public float EndRadius
        {
            get
            {
                return this.endRadiusData.GetValue(this.Level);
            }
        }

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override float Range
        {
            get
            {
                return this.RangeData.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!this.Enabled)
            {
                damage[this.DamageType] = this.GetOrbDamage(unit);
            }

            return damage + this.Owner.GetRawAttackDamage(unit);
        }

        public bool IsTidebringerAnimation(string name)
        {
            return name == "tidebringer" || name == "attack1_gunsword_anim";
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
            if (unit.IsBuilding || !this.CanBeCasted())
            {
                return 0;
            }

            return this.DamageData.GetValue(this.Level);
        }
    }
}