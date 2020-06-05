namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class TargetableAbility : UsableAbility
    {
        public TargetableAbility(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return this.UseAbility(targetManager, comboSleeper, true);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsVisible || target.IsInvulnerable || target.IsReflectingDamage
                || (this.Ability.BreaksLinkens && target.IsBlockingAbilities))
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}