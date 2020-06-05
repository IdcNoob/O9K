namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using TargetManager;

    internal class LegionBlink : BlinkAbility
    {
        public LegionBlink(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            var target = targetManager.Target;
            var position = target.GetPredictedPosition(0.4f).Extend2D(this.Owner.Position, 100);

            if (this.Owner.Distance(position) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var position = target.GetPredictedPosition(0.4f).Extend2D(this.Owner.Position, 100);

            if (this.Owner.Distance(position) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay + 0.1f);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}