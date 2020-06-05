namespace O9K.AIO.Heroes.SpiritBreaker.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class ChargeOfDarkness : TargetableAbility
    {
        public ChargeOfDarkness(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target) + 0.5f;
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}