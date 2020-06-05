namespace O9K.AIO.Heroes.Spectre.Abilities
{
    using System;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class DaggerMove : UsableAbility
    {
        public DaggerMove(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var position = this.Owner.Position.Extend2D(
                Game.MousePosition,
                Math.Max(this.Ability.CastRange, this.Owner.Distance(Game.MousePosition)));

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
    }
}