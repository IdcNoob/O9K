namespace O9K.Core.Entities.Abilities.Base
{
    using System.Collections.Generic;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Prediction.Data;

    public abstract class ConeAbility : LineAbility
    {
        protected SpecialData EndRadiusData;

        protected ConeAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public virtual float EndRadius
        {
            get
            {
                return this.EndRadiusData?.GetValue(this.Level) ?? this.Radius;
            }
        }

        public override float Range
        {
            get
            {
                var range = this.RangeData?.GetValue(this.Level) ?? this.CastRange;
                return range + this.EndRadius;
            }
        }

        public override SkillShotType SkillShotType { get; } = SkillShotType.Cone;

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.EndRadius = this.EndRadius;

            return input;
        }
    }
}