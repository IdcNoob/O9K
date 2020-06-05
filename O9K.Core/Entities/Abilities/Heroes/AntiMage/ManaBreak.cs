namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
    using System;

    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.antimage_mana_break)]
    public class ManaBreak : PassiveAbility //, IHasPassiveDamageIncrease
    {
        private readonly SpecialData burnMultiplierData;

        private readonly SpecialData maxManaBurnData;

        public ManaBreak(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "mana_per_hit");
            this.burnMultiplierData = new SpecialData(baseAbility, "percent_damage_per_burn");
            this.maxManaBurnData = new SpecialData(baseAbility, "mana_per_hit_pct");
        }

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (!unit.IsUnit || unit.IsMagicImmune || unit.IsAlly(this.Owner))
            {
                return new Damage();
            }

            var manaDamage = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = (int)((Math.Min(manaDamage, unit.Mana) * (this.burnMultiplierData.GetValue(this.Level))) / 100f)
            };
        }
    }
}