namespace O9K.AIO.Heroes.Leshrac.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using FailSafe;

    using TargetManager;

    internal class SplitEarth : DisableAbility
    {
        public FailSafe FailSafe;

        public SplitEarth(ActiveAbility ability)
            : base(ability)
        {
        }

        public NukeAbility Storm { get; set; }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            var stormDebuff = targetManager.Target.GetModifier("modifier_leshrac_lightning_storm_slow");

            if (stormDebuff != null)
            {
                if (stormDebuff.RemainingTime < input.Delay)
                {
                    input.Delay += (input.Delay - stormDebuff.RemainingTime) * 3f;
                    this.FailSafe.Sleeper.Sleep(0.3f);
                }
            }
            else
            {
                if (this.Storm.Sleeper.IsSleeping)
                {
                    input.Delay *= 0.5f;
                    this.FailSafe.Sleeper.Sleep(0.3f);
                }
            }

            var output = this.Ability.GetPredictionOutput(input);
            var castPosition = output.CastPosition;

            if (output.HitChance < HitChance.Low || this.Owner.Distance(castPosition) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(castPosition))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(castPosition) + 0.5f;
            var delay = this.Ability.GetCastDelay(castPosition);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}