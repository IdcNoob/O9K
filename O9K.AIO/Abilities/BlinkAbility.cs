namespace O9K.AIO.Abilities
{
    using System;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkAbility : UsableAbility
    {
        public BlinkAbility(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            this.Sleeper.Sleep(this.Ability.GetCastDelay(targetManager.Target) + 0.5f);
            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (targetManager.Target == null)
            {
                return true;
            }

            var target = targetManager.Target;

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (target.HasModifier("modifier_pudge_meat_hook"))
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
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public virtual bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            if (this.Owner.Distance(toPosition) < 200)
            {
                return false;
            }

            var position = this.Owner.Position.Extend2D(toPosition, Math.Min(this.Ability.CastRange - 25, this.Owner.Distance(toPosition)));
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

        public virtual bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
        {
            if (this.Owner.Distance(targetManager.Target) < minDistance)
            {
                return false;
            }

            var position = targetManager.Target.Position.Extend2D(this.Owner.Position, blinkDistance);
            if (this.Owner.Distance(position) > this.Ability.CastRange)
            {
                return false;
            }

            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(position) + 0.1f;
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}