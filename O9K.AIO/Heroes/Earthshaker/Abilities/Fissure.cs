namespace O9K.AIO.Heroes.Earthshaker.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using TargetManager;

    internal class Fissure : DisableAbility
    {
        public Fissure(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var echo = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.earthshaker_echo_slam);

            if (echo?.TimeSinceCasted < 1f)
            {
                if (!this.Ability.UseAbility(target, targetManager.EnemyHeroes, HitChance.Low))
                {
                    return false;
                }
            }
            else
            {
                var fountain = targetManager.EnemyFountain;

                var input = this.Ability.GetPredictionInput(target);
                var output = this.Ability.GetPredictionOutput(input);

                if (output.HitChance < HitChance.Low)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(output.CastPosition.Extend2D(fountain, 200)))
                {
                    return false;
                }
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}