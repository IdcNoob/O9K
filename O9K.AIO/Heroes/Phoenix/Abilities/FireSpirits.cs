namespace O9K.AIO.Heroes.Phoenix.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Prediction.Data;

    using TargetManager;

    internal class FireSpirits : DebuffAbility
    {
        public FireSpirits(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.IsUsable)
            {
                if (this.Ability.UseAbility())
                {
                    comboSleeper.Sleep(0.1f);
                    return true;
                }
            }

            if (!this.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(this.Ability.GetHitTime(targetManager.Target) + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}