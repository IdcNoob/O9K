namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.mars_gods_rebuke)]
    public class GodsRebuke : ConeAbility, INuke
    {
        private readonly SpecialData bonusHeroDamageData;

        public GodsRebuke(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "angle");
            this.EndRadiusData = new SpecialData(baseAbility, "radius");
            this.RangeData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "crit_mult");
            this.bonusHeroDamageData = new SpecialData(baseAbility, "bonus_damage_vs_heroes");
        }

        public override bool CanHitSpellImmuneEnemy { get; } = true;

        public override bool IntelligenceAmplify { get; } = false;

        public override float Range
        {
            get
            {
                return this.RangeData.GetValue(this.Level) - 150;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return base.BaseCastRange - 150;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var crit = this.DamageData.GetValue(this.Level) / 100;
            var bonusDamage = unit.IsHero ? this.bonusHeroDamageData.GetValue(this.Level) : 0;

            return this.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, crit, bonusDamage);
        }
    }
}