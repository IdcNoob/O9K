namespace O9K.AIO.Heroes.Pangolier.Abilities
{
    using System;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using SharpDX;

    using TargetManager;

    using BaseSwashbuckle = Core.Entities.Abilities.Heroes.Pangolier.Swashbuckle;

    internal class SwashbuckleBlink : BlinkAbility
    {
        private readonly BaseSwashbuckle swashbuckle;

        public SwashbuckleBlink(ActiveAbility ability)
            : base(ability)
        {
            this.swashbuckle = (BaseSwashbuckle)ability;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            var position = this.Owner.Position.Extend2D(toPosition, Math.Min(this.Ability.CastRange - 25, this.Owner.Distance(toPosition)));

            var target = targetManager.Target;

            if (target != null)
            {
                if (!this.swashbuckle.UseAbility(position, target.GetPredictedPosition(this.Ability.GetHitTime(target.Position))))
                {
                    return false;
                }
            }
            else
            {
                if (!this.swashbuckle.UseAbility(position, position))
                {
                    return false;
                }
            }

            var delay = this.Ability.GetCastDelay(position);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}