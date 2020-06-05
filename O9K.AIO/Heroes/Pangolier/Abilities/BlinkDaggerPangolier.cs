namespace O9K.AIO.Heroes.Pangolier.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class BlinkDaggerPangolier : BlinkAbility
    {
        public BlinkDaggerPangolier(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Owner.HasModifier("modifier_pangolier_gyroshell"))
            {
                return false;
            }

            var target = targetManager.Target;
            var distance = this.Owner.Distance(target);

            if (distance < 300)
            {
                return false;
            }

            if (target.HasModifier("modifier_pangolier_gyroshell_timeout")
                || (this.Owner.GetAngle(target.Position) < 1.25f && distance < 800))
            {
                return false;
            }

            var position = target.GetPredictedPosition(0.1f);

            if (this.Ability.CastRange < this.Owner.Distance(position))
            {
                return false;
            }

            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}