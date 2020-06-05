namespace O9K.AIO.Heroes.Earthshaker.Abilities
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

    internal class BlinkDaggerShaker : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerShaker(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var totem = usableAbilities.Find(x => x.Ability.Id == AbilityId.earthshaker_enchant_totem);
            if (totem != null)
            {
                var inputTotem = totem.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
                inputTotem.Range += this.Ability.CastRange;
                inputTotem.CastRange = this.Ability.CastRange;
                inputTotem.SkillShotType = SkillShotType.Circle;

                if (this.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
                {
                    inputTotem.AreaOfEffect = false;
                    inputTotem.Delay -= 0.1f;
                }

                var outputTotem = totem.Ability.GetPredictionOutput(inputTotem);
                if (outputTotem.HitChance < HitChance.Low)
                {
                    return false;
                }

                this.blinkPosition = outputTotem.CastPosition;
                if (this.Owner.Distance(this.blinkPosition) > this.Ability.CastRange
                    || this.Owner.Distance(this.blinkPosition) < totem.Ability.Radius - 50)
                {
                    return false;
                }

                return true;
            }

            if (!(usableAbilities.Find(x => x.Ability.Id == AbilityId.earthshaker_echo_slam) is EchoSlam echo))
            {
                return false;
            }

            var input = echo.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Range += this.Ability.CastRange;
            input.CastRange = this.Ability.CastRange;
            input.SkillShotType = SkillShotType.Circle;
            var output = echo.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < echo.TargetsToHit(menu))
            {
                return false;
            }

            this.blinkPosition = output.CastPosition;
            if (this.Owner.Distance(this.blinkPosition) > this.Ability.CastRange
                || output.AoeTargetsHit.Count == 1 && this.Owner.Distance(this.blinkPosition) < 350)
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