namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.earth_spirit_petrify)]
    public class EnchantRemnant : RangedAbility, IShield
    {
        public EnchantRemnant(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.DurationData = new SpecialData(baseAbility, "duration");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public string ShieldModifierName { get; } = "modifier_earthspirit_petrify";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;

        public override int GetDamage(Unit9 unit)
        {
            var healthRegen = unit.HealthRegeneration * this.Duration;
            var damage = this.DamageData.GetValue(this.Level) - healthRegen;
            var amplify = unit.GetDamageAmplification(this.Owner, this.DamageType, true);
            var block = unit.GetDamageBlock(this.DamageType);

            return (int)((damage - block) * amplify);
        }
    }
}