namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
    using System;

    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;
    using Helpers.Range;

    using Metadata;

    [AbilityId(AbilityId.dark_willow_shadow_realm)]
    public class ShadowRealm : ActiveAbility, IShield, IHasRangeIncrease, INuke
    {
        private readonly SpecialData attackRange;

        private readonly SpecialData maxDamageDurationData;

        public ShadowRealm(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackRange = new SpecialData(baseAbility, "attack_range_bonus");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.maxDamageDurationData = new SpecialData(baseAbility, "max_damage_duration");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Untargetable;

        public bool Casted
        {
            get
            {
                return this.Owner.HasModifier(this.ShieldModifierName);
            }
        }

        public bool DamageMaxed
        {
            get
            {
                var modifier = this.Owner.GetModifier(this.ShieldModifierName);
                return modifier?.ElapsedTime >= this.maxDamageDurationData.GetValue(this.Level);
            }
        }

        public bool IsRangeIncreasePermanent { get; } = false;

        public RangeIncreaseType RangeIncreaseType { get; } = RangeIncreaseType.Attack;

        public string RangeModifierName { get; } = "modifier_dark_willow_shadow_realm_buff";

        public float RealmTime
        {
            get
            {
                var modifier = this.Owner.GetModifier(this.ShieldModifierName);
                return modifier?.ElapsedTime ?? 0;
            }
        }

        public string ShieldModifierName { get; } = "modifier_dark_willow_shadow_realm_buff";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float GetRangeIncrease(Unit9 unit, RangeIncreaseType type)
        {
            return this.attackRange.GetValue(this.Level);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            var modifier = this.Owner.GetModifier(this.ShieldModifierName);
            if (modifier == null)
            {
                return damage;
            }

            var timeMultiplier = Math.Min(modifier.ElapsedTime / this.maxDamageDurationData.GetValue(this.Level), 1);
            damage[this.DamageType] = this.DamageData.GetValue(this.Level) * timeMultiplier;

            return damage + this.Owner.GetRawAttackDamage(unit);
        }
    }
}