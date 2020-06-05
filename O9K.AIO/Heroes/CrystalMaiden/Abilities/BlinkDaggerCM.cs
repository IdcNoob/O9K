namespace O9K.AIO.Heroes.CrystalMaiden.Abilities
{
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerCM : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerCM(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var field = (FreezingField)usableAbilities.Find(x => x.Ability.Id == AbilityId.crystal_maiden_freezing_field);
            if (field == null)
            {
                return false;
            }

            var input = field.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Range += this.Ability.CastRange;
            input.CastRange = this.Ability.CastRange;
            input.SkillShotType = SkillShotType.Circle;
            input.Radius *= 0.7f;
            var output = field.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < field.TargetsToHit(menu))
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

            //comboSleeper.Sleep(0.3f);
            this.OrbwalkSleeper.Sleep(0.5f);
            this.Sleeper.Sleep(0.5f);
            return true;
        }
    }
}