namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.razor_plasma_field)]
    public class PlasmaField : AreaOfEffectAbility, INuke, IDebuff
    {
        private readonly SpecialData maxDamageData;

        private readonly SpecialData minDamageData;

        public PlasmaField(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.minDamageData = new SpecialData(baseAbility, "damage_min");
            this.maxDamageData = new SpecialData(baseAbility, "damage_max");
        }

        public string DebuffModifierName { get; } = "modifier_razor_plasma_field_slow";

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var distance = Math.Max(unit.Distance(this.Owner) - 100, 0);

            var max = this.maxDamageData.GetValue(this.Level);
            var min = this.minDamageData.GetValue(this.Level);

            var damage = Math.Max(Math.Min((distance / this.Radius) * max, max), min);

            return new Damage
            {
                [this.DamageType] = damage
            };
        }
    }
}