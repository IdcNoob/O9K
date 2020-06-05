namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class DisableAbility : UsableAbility
    {
        public DisableAbility(ActiveAbility ability)
            : base(ability)
        {
            this.Disable = (IDisable)ability;
        }

        protected IDisable Disable { get; }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
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

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (this.Disable.UnitTargetCast)
                {
                    return false;
                }

                if (!this.ChainStun(target, true))
                {
                    return false;
                }
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

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (aoe)
            {
                if (!this.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, HitChance.Low))
                {
                    return false;
                }
            }
            else
            {
                if (!this.Ability.UseAbility(targetManager.Target, HitChance.Low))
                {
                    return false;
                }
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}