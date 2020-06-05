namespace O9K.Core.Entities.Abilities.Base
{
    using System.Collections.Generic;

    using Ensage;

    using Entities.Units;

    using Prediction.Data;

    public abstract class RangedAreaOfEffectAbility : PredictionAbility
    {
        protected RangedAreaOfEffectAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override SkillShotType SkillShotType { get; } = SkillShotType.RangedAreaOfEffect;

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            return this.UseAbility(target, queue, bypass);
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            return this.UseAbility(mainTarget, queue, bypass);
        }
    }
}