namespace O9K.AIO.Heroes.Magnus.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerMagnus : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerMagnus(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            if (!(usableAbilities.Find(x => x.Ability.Id == AbilityId.magnataur_reverse_polarity) is ReversePolarity polarity))
            {
                return false;
            }

            var input = polarity.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Range += this.Ability.CastRange;
            input.CastRange = this.Ability.CastRange;
            input.SkillShotType = SkillShotType.Circle;
            var output = polarity.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < polarity.TargetsToHit(menu))
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

            comboSleeper.Sleep(0.3f);
            this.OrbwalkSleeper.Sleep(0.5f);
            this.Sleeper.Sleep(this.Ability.GetCastDelay(targetManager.Target) + 0.5f);
            return true;
        }
    }
}