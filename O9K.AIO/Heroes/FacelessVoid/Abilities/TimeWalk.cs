namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
    using System;
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class TimeWalk : BlinkAbility
    {
        private Vector3 blinkPosition;

        public TimeWalk(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var chrono = (Chronosphere)usableAbilities.Find(x => x.Ability.Id == AbilityId.faceless_void_chronosphere);
            if (chrono == null)
            {
                if (this.Owner.Distance(targetManager.Target) < 300)
                {
                    return false;
                }

                this.blinkPosition = Vector3.Zero;
                return true;
            }

            var input = chrono.Ability.GetPredictionInput(targetManager.Target, EntityManager9.EnemyHeroes);
            input.CastRange += this.Ability.CastRange;
            input.Range += this.Ability.CastRange;
            var output = chrono.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < chrono.TargetsToHit(menu))
            {
                return false;
            }

            var range = Math.Min(this.Ability.CastRange, this.Owner.Distance(output.CastPosition) - 200);
            this.blinkPosition = this.Owner.Position.Extend2D(output.CastPosition, range);

            if (this.Owner.Distance(this.blinkPosition) > this.Ability.CastRange)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.blinkPosition.IsZero)
            {
                var input = this.Ability.GetPredictionInput(targetManager.Target);
                input.Delay += 0.5f;
                var output = this.Ability.GetPredictionOutput(input);

                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                this.blinkPosition = output.CastPosition;
            }

            if (!this.Ability.UseAbility(this.blinkPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(this.blinkPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}