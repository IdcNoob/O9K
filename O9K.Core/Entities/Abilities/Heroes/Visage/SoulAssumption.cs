namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
    using Base;
    using Base.Types;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.visage_soul_assumption)]
    public class SoulAssumption : RangedAbility, INuke
    {
        public SpecialData bonusDamageData;

        public SpecialData stackLimit;

        public SoulAssumption(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
            this.DamageData = new SpecialData(baseAbility, "soul_base_damage");
            this.bonusDamageData = new SpecialData(baseAbility, "soul_charge_damage");
            this.stackLimit = new SpecialData(baseAbility, "stack_limit");
        }

        public bool MaxCharges
        {
            get
            {
                return (this.Owner.GetModifier("modifier_visage_soul_assumption")?.StackCount ?? 0) >= this.stackLimit.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var bonus = this.bonusDamageData.GetValue(this.Level);
            var stacks = this.Owner.BaseUnit.GetModifierByName("modifier_visage_soul_assumption")?.StackCount ?? 0;

            damage[this.DamageType] += stacks * bonus;

            return damage;
        }
    }
}