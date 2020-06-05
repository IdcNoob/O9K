namespace O9K.Core.Entities.Abilities.Heroes.VoidSpirit
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.void_spirit_astral_step)]
    public class AstralStep : LineAbility, INuke, IBlink
    {
        private readonly SpecialData castRangeData;

        private readonly SpecialData critData;

        public AstralStep(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "pop_damage");
            this.castRangeData = new SpecialData(baseAbility, "max_travel_distance");
            this.critData = new SpecialData(AbilityId.special_bonus_unique_void_spirit_8, "value");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override DamageType DamageType { get; } = DamageType.Physical;

        public override float Range
        {
            get
            {
                return this.CastRange;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = new PredictionInput9
            {
                Caster = this.Owner,
                Target = target,
                CollisionTypes = this.CollisionTypes,
                Delay = this.CastPoint + this.ActivationDelay + InputLag,
                Speed = this.Speed,
                Range = this.Range,
                Radius = 0,
                CastRange = this.CastRange,
                SkillShotType = this.SkillShotType,
                RequiresToTurn = !this.NoTargetCast
            };

            if (aoeTargets != null)
            {
                input.AreaOfEffect = this.HasAreaOfEffect;
                input.AreaOfEffectTargets = aoeTargets;
            }

            return input;
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var critMultiplier = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_void_spirit_8)?.Level > 0
                                     ? this.critData.GetValue(this.Level) / 100f
                                     : 1f;

            var damage = this.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, critMultiplier);
            damage[DamageType.Magical] = this.DamageData.GetValue(this.Level) * 0.8f;

            return damage;
        }
    }
}