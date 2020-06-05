namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.monkey_king_boundless_strike)]
    public class BoundlessStrike : LineAbility, IDisable, INuke
    {
        public BoundlessStrike(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "strike_radius");
            this.DamageData = new SpecialData(baseAbility, "strike_crit_mult");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool IntelligenceAmplify { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var crit = this.DamageData.GetValue(this.Level) / 100;
            return this.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, crit);
        }
    }
}