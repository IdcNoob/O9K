namespace O9K.AIO.Heroes.VoidSpirit.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using TargetManager;

    internal class AetherRemnant : DisableAbility
    {
        private readonly Core.Entities.Abilities.Heroes.VoidSpirit.AetherRemnant remnant;

        public AetherRemnant(ActiveAbility ability)
            : base(ability)
        {
            this.remnant = (Core.Entities.Abilities.Heroes.VoidSpirit.AetherRemnant)ability;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            var position = targetManager.Target.Position;

            if (!this.remnant.UseAbility(position.Extend2D(this.Owner.Position, 100), position))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return true;
            }

            if (this.Ability.GetDamage(target) < target.Health)
            {
                if (target.IsStunned)
                {
                    return this.ChainStun(target, false);
                }

                if (target.IsHexed)
                {
                    return this.ChainStun(target, false);
                }

                if (target.IsSilenced)
                {
                    return !this.Disable.IsSilence(false) || this.ChainStun(target, false);
                }

                if (target.IsRooted)
                {
                    return !this.Disable.IsRoot() || this.ChainStun(target, false);
                }
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            return true;
        }
    }
}