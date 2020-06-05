namespace O9K.Core.Entities.Abilities.Base
{
    using System.Collections.Generic;
    using System.Linq;

    using Components;

    using Ensage;

    using Entities.Units;

    using Prediction.Data;

    public abstract class AreaOfEffectAbility : ActiveAbility, IHasRadius
    {
        protected AreaOfEffectAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float Range
        {
            get
            {
                return this.CastRange + this.Radius;
            }
        }

        public override SkillShotType SkillShotType { get; } = SkillShotType.AreaOfEffect;

        public override bool CanHit(Unit9 target)
        {
            if (target.IsMagicImmune && ((target.IsEnemy(this.Owner) && !this.CanHitSpellImmuneEnemy)
                                         || (target.IsAlly(this.Owner) && !this.CanHitSpellImmuneAlly)))
            {
                return false;
            }

            var predictionInput = this.GetPredictionInput(target);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.HitChance <= HitChance.Impossible)
            {
                return false;
            }

            return true;
        }

        public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
        {
            var predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.HitChance <= HitChance.Impossible)
            {
                return false;
            }

            var magicImmune = aoeTargets.Count(
                x => x.IsMagicImmune && ((x.IsEnemy(this.Owner) && !this.CanHitSpellImmuneEnemy)
                                         || (x.IsAlly(this.Owner) && !this.CanHitSpellImmuneAlly)));

            if (output.AoeTargetsHit.Count - magicImmune < minCount)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            var predictionInput = this.GetPredictionInput(target);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.HitChance < minimumChance)
            {
                return false;
            }

            return this.UseAbility(queue, bypass);
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            var predictionInput = this.GetPredictionInput(mainTarget, aoeTargets);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.AoeTargetsHit.Count < minAOETargets)
            {
                return false;
            }

            if (output.HitChance < minimumChance)
            {
                return false;
            }

            return this.UseAbility(queue, bypass);
        }
    }
}