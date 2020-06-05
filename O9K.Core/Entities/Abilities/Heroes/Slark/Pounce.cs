namespace O9K.Core.Entities.Abilities.Heroes.Slark
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    using SharpDX;

    [AbilityId(AbilityId.slark_pounce)]
    public class Pounce : LineAbility, IBlink
    {
        private readonly SpecialData castRangeData;

        private readonly SpecialData scepterRangeData;

        public Pounce(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "pounce_radius");
            this.SpeedData = new SpecialData(baseAbility, "pounce_speed");
            this.castRangeData = new SpecialData(baseAbility, "pounce_distance");
            this.scepterRangeData = new SpecialData(baseAbility, "pounce_distance_scepter");
        }

        public BlinkType BlinkType { get; } = BlinkType.Leap;

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterRangeData.GetValue(this.Level);
                }

                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override bool HasAreaOfEffect { get; } = false;

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        public override float Speed
        {
            get
            {
                var speed = this.SpeedData.GetValue(this.Level);

                if (this.Owner.HasAghanimsScepter)
                {
                    speed *= 2f;
                }

                return speed;
            }
        }

        public override bool CanHit(Unit9 target)
        {
            if (target.IsMagicImmune && ((target.IsEnemy(this.Owner) && !this.CanHitSpellImmuneEnemy)
                                         || (target.IsAlly(this.Owner) && !this.CanHitSpellImmuneAlly)))
            {
                return false;
            }

            var predictionInput = this.GetPredictionInput(target);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.HitChance <= HitChance.Impossible || this.Owner.GetAngle(output.CastPosition) > 0.1f)
            {
                return false;
            }

            return true;
        }

        public override float GetHitTime(Vector3 position)
        {
            return this.GetCastDelay(position) + this.ActivationDelay + (this.Range / this.Speed);
        }

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.CastRange -= 100;
            input.Range -= 100;

            return input;
        }
    }
}