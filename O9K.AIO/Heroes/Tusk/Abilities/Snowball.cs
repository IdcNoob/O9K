namespace O9K.AIO.Heroes.Tusk.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class Snowball : TargetableAbility
    {
        public Snowball(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(targetManager.Target))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);

            if (!this.Ability.IsUsable)
            {
                delay += 0.5f;
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.1f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}