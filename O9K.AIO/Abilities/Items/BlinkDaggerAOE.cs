namespace O9K.AIO.Abilities.Items
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerAOE : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerAOE(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var ability = usableAbilities.FirstOrDefault();
            if (ability == null)
            {
                return false;
            }

            var input = ability.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Range += this.Ability.CastRange;
            input.CastRange = this.Ability.CastRange;
            input.SkillShotType = SkillShotType.Circle;
            var output = ability.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || output.TargetPosition.Distance2D(output.CastPosition) > ability.Ability.Radius * 0.9f)
            {
                return false;
            }

            this.blinkPosition = output.CastPosition;
            if (this.Owner.Distance(this.blinkPosition) > this.Ability.CastRange)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.blinkPosition))
            {
                return false;
            }

            var delay = this.Ability.GetHitTime(this.blinkPosition);
            comboSleeper.Sleep(delay + 0.1f);
            this.OrbwalkSleeper.Sleep(delay + 0.2f);
            this.Sleeper.Sleep(this.Ability.GetCastDelay(targetManager.Target) + 0.5f);
            return true;
        }
    }
}