namespace O9K.Core.Entities.Abilities.Items
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

    [AbilityId(AbilityId.item_diffusal_blade)]
    public class DiffusalBlade : RangedAbility, IDebuff, IHasPassiveDamageIncrease
    {
        private readonly SpecialData burnMultiplierData;

        public DiffusalBlade(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "feedback_mana_burn");
            this.burnMultiplierData = new SpecialData(baseAbility, "damage_per_burn");
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public string DebuffModifierName { get; } = "modifier_item_diffusal_blade_slow";

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (!this.IsUsable || unit.IsMagicImmune || unit.IsAlly(this.Owner))
            {
                return new Damage();
            }

            var manaDamage = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = (int)(Math.Min(manaDamage, unit.Mana) * this.burnMultiplierData.GetValue(this.Level))
            };
        }
    }
}