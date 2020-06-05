namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.leshrac_pulse_nova)]
    public class PulseNova : ToggleAbility, IHasRadius
    {
        public PulseNova(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
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

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = new PredictionInput9
            {
                Caster = this.Owner,
                Target = target,
                CollisionTypes = this.CollisionTypes,
                Delay = this.CastPoint + this.ActivationDelay + InputLag,
                Speed = this.Speed,
                CastRange = this.CastRange,
                Range = this.Range,
                Radius = this.Radius,
                SkillShotType = this.SkillShotType
            };

            if (aoeTargets != null)
            {
                input.AreaOfEffect = this.HasAreaOfEffect;
                input.AreaOfEffectTargets = aoeTargets;
            }

            return input;
        }
    }
}