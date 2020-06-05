namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
    using System;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using SharpDX;

    using TargetManager;

    internal class FireRemnantBlink : BlinkAbility
    {
        public FireRemnantBlink(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
        {
            if (this.Owner.Distance(toPosition) < 200)
            {
                return false;
            }

            var position = this.Owner.Position.Extend2D(toPosition, Math.Min(this.Ability.CastRange - 25, this.Owner.Distance(toPosition)));
            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(toPosition);
            var hitTime = this.Ability.GetHitTime(toPosition);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime * 0.5f);
            this.OrbwalkSleeper.Sleep(delay);

            return true;
        }
    }
}