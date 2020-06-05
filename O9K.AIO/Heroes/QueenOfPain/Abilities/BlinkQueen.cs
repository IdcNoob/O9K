namespace O9K.AIO.Heroes.QueenOfPain.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage.SDK.Geometry;

    using TargetManager;

    internal class BlinkQueen : BlinkAbility
    {
        public BlinkQueen(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(input);

            var blinkPosition = output.CastPosition.Extend2D(this.Owner.Position, 300);

            if (this.Owner.Distance(target.Position) < blinkPosition.Distance2D(target.Position))
            {
                return false;
            }

            if (this.Owner.Distance(blinkPosition) > this.Ability.CastRange || !this.Ability.UseAbility(blinkPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(blinkPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}