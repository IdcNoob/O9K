namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.axe_culling_blade)]
    public class CullingBlade : RangedAbility, INuke
    {
        private readonly SpecialData killThresholdData;

        public CullingBlade(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.killThresholdData = new SpecialData(baseAbility, "kill_threshold");
        }

        protected override float BaseCastRange
        {
            get
            {
                return base.BaseCastRange + 100;
            }
        }

        public override int GetDamage(Unit9 unit)
        {
            var threshold = this.killThresholdData.GetValue(this.Level);
            var healthRegen = unit.HealthRegeneration * this.GetCastDelay(unit) * 1.5f;

            if (unit.Health + healthRegen < threshold)
            {
                //todo should ignore amplifiers ?
                return (int)unit.MaximumHealth;
            }

            var damage = this.DamageData.GetValue(this.Level);
            var amplify = unit.GetDamageAmplification(this.Owner, this.DamageType, true);
            var block = unit.GetDamageBlock(this.DamageType);

            return (int)((damage - block) * amplify);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [DamageType.HealthRemoval] = this.killThresholdData.GetValue(this.Level)
            };
        }
    }
}