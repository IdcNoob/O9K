namespace O9K.Core.Entities.Abilities.Heroes.Pangolier
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Extensions;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    using Prediction.Data;

    using SharpDX;

    [AbilityId(AbilityId.pangolier_swashbuckle)]
    public class Swashbuckle : LineAbility, IBlink, INuke
    {
        private readonly SpecialData strikesData;

        public Swashbuckle(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve range in prediction

            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.SpeedData = new SpecialData(baseAbility, "dash_speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RangeData = new SpecialData(baseAbility, "range");
            this.strikesData = new SpecialData(baseAbility, "strikes");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override bool CanHitSpellImmuneEnemy { get; } = true;

        public override bool IntelligenceAmplify { get; } = false;

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.UseBlink = true;
            input.AreaOfEffect = false;
            return input;
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var passiveDamage = this.Owner.GetOnHitEffectDamage(unit);

            return (damage + passiveDamage) * this.strikesData.GetValue(this.Level);
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            var predictionInput = this.GetPredictionInput(target);
            var output = this.GetPredictionOutput(predictionInput);

            if (output.HitChance < minimumChance)
            {
                return false;
            }

            return this.UseAbility(output.BlinkLinePosition, output.CastPosition, queue, bypass);
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

            return this.UseAbility(output.BlinkLinePosition, output.CastPosition, queue, bypass);
        }

        public bool UseAbility(Vector3 startPosition, Vector3 direction, bool queue = false, bool bypass = false)
        {
            if (!this.BaseAbility.TargetPosition(startPosition, queue, bypass)
                || !this.BaseAbility.TargetPosition(direction, queue, bypass))
            {
                return false;
            }

            var result = this.BaseAbility.UseAbility(startPosition, queue, bypass);

            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }

        public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
        {
            //todo queue ?
            // simple position to get as close as possible to target
            var distance = Math.Max(this.Owner.GetAttackRange(), this.Owner.Distance(position) - this.CastRange);
            var startPosition = position.Extend2D(this.Owner.Position, distance);

            return this.UseAbility(startPosition, position, queue, bypass);
        }
    }
}