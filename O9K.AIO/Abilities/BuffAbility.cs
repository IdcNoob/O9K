namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class BuffAbility : UsableAbility
    {
        public BuffAbility(ActiveAbility ability)
            : base(ability)
        {
            this.Buff = (IBuff)ability;
        }

        public IBuff Buff { get; }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(targetManager.Owner))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Owner);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = this.Owner;

            if (target.IsInvulnerable)
            {
                return false;
            }

            if (target.Equals(this.Buff.Owner))
            {
                if (!this.Buff.BuffsOwner)
                {
                    return false;
                }
            }
            else
            {
                if (!this.Buff.BuffsAlly)
                {
                    return false;
                }
            }

            if (target.IsMagicImmune && !this.Buff.PiercesMagicImmunity(target))
            {
                return false;
            }

            if (target.HasModifier(this.Buff.BuffModifierName))
            {
                return false;
            }

            if (this.Buff is ToggleAbility toggle && toggle.Enabled)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Owner, targetManager.AllyHeroes, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Owner);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float distance)
        {
            if (this.Owner.Distance(targetManager.Target) > distance)
            {
                return false;
            }

            return this.UseAbility(targetManager, comboSleeper, true);
        }
    }
}