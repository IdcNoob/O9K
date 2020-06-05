namespace O9K.AIO.Heroes.Earthshaker.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class EnchantTotem : DisableAbility
    {
        public EnchantTotem(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility())
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(this.Owner) + 0.5f;
            var delay = this.Ability.GetCastDelay(this.Owner);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.Owner.HasAghanimsScepter && this.Owner.Distance(targetManager.Target) < this.Ability.Radius - 100)
            {
                if (!this.Ability.UseAbility())
                {
                    return false;
                }
            }
            else
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