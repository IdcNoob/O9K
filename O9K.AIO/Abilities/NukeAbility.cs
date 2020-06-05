namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class NukeAbility : UsableAbility
    {
        protected string[] breakShields =
        {
            "modifier_ember_spirit_flame_guard",
            "modifier_item_pipe_barrier",
            "modifier_abaddon_aphotic_shield",
            "modifier_oracle_false_promise_timer",
        };

        public NukeAbility(ActiveAbility ability)
            : base(ability)
        {
        }

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

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.IsReflectingDamage)
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            var damage = this.Ability.GetDamage(target);
            if (damage <= 0 && !target.HasModifier(this.breakShields))
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (this.Ability.UnitTargetCast)
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