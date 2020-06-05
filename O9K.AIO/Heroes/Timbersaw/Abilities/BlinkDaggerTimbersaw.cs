namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerTimbersaw : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerTimbersaw(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.HasModifier("modifier_shredder_timber_chain"))
            {
                return false;
            }

            return true;
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var target = targetManager.Target;

            if (this.Owner.Distance(target) < 600)
            {
                return false;
            }

            var chain = usableAbilities.FirstOrDefault(x => x.Ability.Id == AbilityId.shredder_timber_chain);
            if (chain == null)
            {
                return true;
            }

            this.blinkPosition = target.Position;
            var whirlingDeath = usableAbilities.FirstOrDefault(x => x.Ability.Id == AbilityId.shredder_whirling_death);
            if (whirlingDeath != null)
            {
                var input = whirlingDeath.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
                input.Range += this.Ability.CastRange;
                input.CastRange = this.Ability.CastRange;
                input.SkillShotType = SkillShotType.Circle;
                var output = whirlingDeath.Ability.GetPredictionOutput(input);
                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                this.blinkPosition = output.CastPosition;
            }

            if (this.Owner.Distance(this.blinkPosition) < this.Ability.CastRange)
            {
                return true;
            }

            return false;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.blinkPosition))
            {
                return false;
            }

            this.Sleeper.Sleep(this.Ability.GetCastDelay(targetManager.Target));
            return true;
        }
    }
}