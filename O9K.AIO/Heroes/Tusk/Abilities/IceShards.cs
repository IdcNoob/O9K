namespace O9K.AIO.Heroes.Tusk.Abilities
{
    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class IceShards : NukeAbility
    {
        public IceShards(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            if (this.Owner.IsInvulnerable && this.Owner.Distance(targetManager.Target) > 800)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target);
            input.Delay += 0.5f;
            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition.Extend2D(this.Ability.Owner.Position, -200)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(output.CastPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}