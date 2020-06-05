namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class HurricanePike : ForceStaff
    {
        public HurricanePike(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
        {
            var target = targetManager.Target;

            if (target.IsRanged)
            {
                return false;
            }

            if (target.IsLinkensProtected || target.IsInvulnerable || target.IsUntargetable)
            {
                return false;
            }

            if (target.IsStunned || target.IsHexed || target.IsRooted || target.IsDisarmed)
            {
                return false;
            }

            if (!this.Ability.CanHit(target))
            {
                return false;
            }

            if (this.Owner.Distance(target) > 350)
            {
                return false;
            }

            if (!this.Ability.UseAbility(target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay();
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);

            return true;
        }
    }
}