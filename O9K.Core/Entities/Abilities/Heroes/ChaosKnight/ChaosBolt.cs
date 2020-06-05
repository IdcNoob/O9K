namespace O9K.Core.Entities.Abilities.Heroes.ChaosKnight
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.chaos_knight_chaos_bolt)]
    public class ChaosBolt : RangedAbility, IDisable, INuke
    {
        private readonly SpecialData maxDamageData;

        public ChaosBolt(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "chaos_bolt_speed");
            this.DamageData = new SpecialData(baseAbility, "damage_min");
            this.maxDamageData = new SpecialData(baseAbility, "damage_max");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = (int)((this.DamageData.GetValue(this.Level) + this.maxDamageData.GetValue(this.Level)) / 2)
            };
        }
    }
}