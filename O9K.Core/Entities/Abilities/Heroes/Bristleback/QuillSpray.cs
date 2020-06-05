namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.bristleback_quill_spray)]
    public class QuillSpray : AreaOfEffectAbility, INuke, IDebuff
    {
        private readonly SpecialData maxDamageData;

        private readonly SpecialData stackDamageData;

        public QuillSpray(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "quill_base_damage");
            this.stackDamageData = new SpecialData(baseAbility, "quill_stack_damage");
            this.maxDamageData = new SpecialData(baseAbility, "max_damage");
        }

        public string DebuffModifierName { get; } = string.Empty;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var baseDamage = this.DamageData.GetValue(this.Level);
            var stackDamage = this.stackDamageData.GetValue(this.Level);
            var stacks = unit.GetModifierStacks("modifier_bristleback_quill_spray");

            return new Damage
            {
                [this.DamageType] = Math.Min(baseDamage + (stacks * stackDamage), this.maxDamageData.GetValue(this.Level))
            };
        }
    }
}