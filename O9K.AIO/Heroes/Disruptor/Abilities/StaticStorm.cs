namespace O9K.AIO.Heroes.Disruptor.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using SharpDX;

    using TargetManager;

    internal class StaticStorm : DisableAbility
    {
        public StaticStorm(ActiveAbility ability)
            : base(ability)
        {
        }

        public bool UseAbility(Vector3 position, TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(position);
            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}