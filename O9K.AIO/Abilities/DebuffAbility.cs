namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using TargetManager;

    internal class DebuffAbility : UsableAbility
    {
        private readonly MultiSleeper debuffSleeper = new MultiSleeper();

        private readonly MultiSleeper visibleSleeper = new MultiSleeper();

        public DebuffAbility(ActiveAbility ability)
            : base(ability)
        {
            this.Debuff = (IDebuff)ability;
        }

        protected IDebuff Debuff { get; }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;
            var isVisible = target.IsVisible;

            if (this.Ability.UnitTargetCast && !isVisible)
            {
                return false;
            }

            if (this.Ability.Id == AbilityId.item_diffusal_blade && target.GetImmobilityDuration() > 0)
            {
                return false;
            }

            if (isVisible)
            {
                if (this.visibleSleeper.IsSleeping(target.Handle))
                {
                    return false;
                }

                var modifier = target.GetModifier(this.Debuff.DebuffModifierName);
                if (modifier != null)
                {
                    var remainingTime = modifier.RemainingTime;
                    if (remainingTime == 0)
                    {
                        return false;
                    }

                    var time = remainingTime - this.Ability.GetHitTime(target);
                    if (time > 0)
                    {
                        this.debuffSleeper.Sleep(target.Handle, time);
                        return false;
                    }
                }
            }
            else
            {
                this.visibleSleeper.Sleep(target.Handle, 0.1f);

                if (this.debuffSleeper.IsSleeping(target.Handle))
                {
                    return false;
                }
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
                if (this.Debuff.UnitTargetCast)
                {
                    return false;
                }

                if (!this.ChainStun(target, true))
                {
                    return false;
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
            if (!this.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}