namespace O9K.Core.Entities.Abilities.Heroes.VoidSpirit
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Extensions;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    using SharpDX;

    [AbilityId(AbilityId.void_spirit_aether_remnant)]
    public class AetherRemnant : LineAbility, IDisable, IAppliesImmobility
    {
        public AetherRemnant(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "activation_delay");
            this.DamageData = new SpecialData(baseAbility, "impact_damage");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.RadiusData = new SpecialData(baseAbility, "remnant_watch_radius");
            this.RangeData = new SpecialData(baseAbility, "remnant_watch_distance");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool HasAreaOfEffect { get; } = false;

        public string ImmobilityModifierName { get; } = "modifier_void_spirit_aether_remnant_pull";

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.UseBlink = true;
            return input;
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

        public bool UseAbility(Vector3 startPosition, Vector3 direction, bool queue = false, bool bypass = false)
        {
            ////make remnant stay behind the target
            //var fixStartPosition = direction.Extend2D(startPosition, -100);
            //direction = startPosition;
            //startPosition = fixStartPosition;

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