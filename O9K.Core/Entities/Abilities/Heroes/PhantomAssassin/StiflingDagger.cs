namespace O9K.Core.Entities.Abilities.Heroes.PhantomAssassin
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.phantom_assassin_stifling_dagger)]
    public class StiflingDagger : RangedAbility, INuke, IDebuff
    {
        private readonly SpecialData multiplierData;

        public StiflingDagger(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "base_damage");
            this.SpeedData = new SpecialData(baseAbility, "dagger_speed");
            this.multiplierData = new SpecialData(baseAbility, "attack_factor_tooltip");
        }

        public string DebuffModifierName { get; } = "modifier_phantom_assassin_stiflingdagger";

        public override bool IntelligenceAmplify { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var autoAttackDamage = this.Owner.GetRawAttackDamage(unit);
            var multiplier = this.multiplierData.GetValue(this.Level) / 100;

            autoAttackDamage[this.DamageType] *= multiplier;

            return damage + autoAttackDamage;
        }
    }
}