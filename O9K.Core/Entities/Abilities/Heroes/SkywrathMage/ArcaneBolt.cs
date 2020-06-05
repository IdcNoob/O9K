namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.skywrath_mage_arcane_bolt)]
    public class ArcaneBolt : RangedAbility, INuke
    {
        private readonly SpecialData damageMultiplierData;

        public ArcaneBolt(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
            this.DamageData = new SpecialData(baseAbility, "bolt_damage");
            this.damageMultiplierData = new SpecialData(baseAbility, "int_multiplier");
        }

        public override bool CanHitSpellImmuneEnemy
        {
            get
            {
                if (this.Owner.GetAbilityById((AbilityId)411)?.Level > 0)
                {
                    return true;
                }

                return base.CanHitSpellImmuneEnemy;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var multiplier = this.damageMultiplierData.GetValue(this.Level);

            damage[this.DamageType] += (int)(this.Owner.TotalIntelligence * multiplier);

            return damage;
        }
    }
}