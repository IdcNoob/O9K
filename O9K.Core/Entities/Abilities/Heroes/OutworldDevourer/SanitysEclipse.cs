namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.obsidian_destroyer_sanity_eclipse)]
    public class SanitysEclipse : CircleAbility, INuke
    {
        private readonly SpecialData damageMultiplierData;

        public SanitysEclipse(Ability baseAbility)
            : base(baseAbility)
        {
            //todo calc dmg into astrall
            this.DamageData = new SpecialData(baseAbility, "base_damage");
            this.damageMultiplierData = new SpecialData(baseAbility, "damage_multiplier");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var baseDamage = base.GetRawDamage(unit, remainingHealth);
            var manaDifference = Math.Max(this.Owner.MaximumMana - unit.MaximumMana, 0);

            baseDamage[this.DamageType] += manaDifference * this.damageMultiplierData.GetValue(this.Level);

            return baseDamage;
        }
    }
}