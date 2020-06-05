namespace O9K.AIO.Heroes.Leshrac.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class PulseNova : UsableAbility
    {
        private readonly ToggleAbility pulseNova;

        public PulseNova(ActiveAbility ability)
            : base(ability)
        {
            this.pulseNova = (ToggleAbility)ability;
        }

        public bool AutoToggle(TargetManager targetManager)
        {
            if (this.pulseNova.Enabled)
            {
                if (!this.ShouldCast(targetManager))
                {
                    return this.UseAbility(null, null, false);
                }
            }
            else
            {
                if (this.ShouldCast(targetManager))
                {
                    return this.UseAbility(null, null, false);
                }
            }

            return false;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!this.pulseNova.CanHit(target))
            {
                return false;
            }

            if (this.Ability.GetDamage(target) <= 0)
            {
                return false;
            }

            if (target.IsReflectingDamage && !this.Owner.IsMagicImmune)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            this.pulseNova.Enabled = !this.pulseNova.Enabled;
            this.Sleeper.Sleep(this.Ability.GetCastDelay() + 0.5f);
            return true;
        }
    }
}