namespace O9K.AIO.Heroes.Pudge.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class Rot : UsableAbility
    {
        private readonly ToggleAbility rot;

        public Rot(ActiveAbility ability)
            : base(ability)
        {
            this.rot = (ToggleAbility)ability;
        }

        public bool IsEnabled
        {
            get
            {
                return this.rot.Enabled;
            }
        }

        public bool AutoToggle(TargetManager targetManager)
        {
            if (this.rot.Enabled)
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

            if (!this.rot.CanHit(target))
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
            this.rot.Enabled = !this.rot.Enabled;
            this.Sleeper.Sleep(0.1f);
            return true;
        }
    }
}