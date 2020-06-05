namespace O9K.Core.Entities.Abilities.Heroes.Zeus
{
    using System;

    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.zuus_static_field)]
    public class StaticField : PassiveAbility, IHasRadius
    {
        public StaticField(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_health_pct");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var health = Math.Max(0, remainingHealth ?? unit.Health);

            return new Damage
            {
                [this.DamageType] = (int)((health * this.DamageData.GetValue(this.Level)) / 100)
            };
        }
    }
}