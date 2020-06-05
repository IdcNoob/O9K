namespace O9K.AIO.Heroes.Slark.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Modes.Combo;

    using TargetManager;

    internal class Pounce : UntargetableAbility
    {
        public Pounce(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            var target = targetManager.Target;

            if (target.HasModifier("modifier_slark_pounce_leash", "modifier_item_diffusal_blade_slow"))
            {
                return false;
            }

            if (target.Distance(this.Owner) < 300 && target.GetAngle(this.Owner) < 1)
            {
                return false;
            }

            return true;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;

            if (target.IsMagicImmune)
            {
                return false;
            }

            var predictionInput = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(predictionInput);

            if (output.HitChance <= HitChance.Impossible)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target);
            var output = this.Ability.GetPredictionOutput(input);

            if (this.Owner.GetAngle(output.CastPosition) > 0.1f)
            {
                this.Owner.BaseUnit.Move(output.CastPosition);
                this.OrbwalkSleeper.Sleep(0.1f);
                comboSleeper.Sleep(0.1f);
                return true;
            }

            if (!this.Ability.UseAbility())
            {
                return false;
            }

            var delay = this.Ability.GetHitTime(targetManager.Target);

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 1f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}