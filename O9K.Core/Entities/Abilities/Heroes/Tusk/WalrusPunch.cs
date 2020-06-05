namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.tusk_walrus_punch)]
    public class WalrusPunch : RangedAbility, INuke
    {
        public WalrusPunch(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "crit_multiplier");
        }

        public override float CastPoint
        {
            get
            {
                return this.Owner.GetAttackPoint();
            }
        }

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public override bool IntelligenceAmplify { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var crit = this.DamageData.GetValue(this.Level) / 100;
            return this.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, crit);
        }
    }
}