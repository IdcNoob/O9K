namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class StoneRemnantBlink : TargetableAbility
    {
        public StoneRemnantBlink(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return true;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.Owner.InFront(150)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(this.Owner.Position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(2);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}