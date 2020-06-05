namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
    using System;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage.SDK.Geometry;

    using SharpDX;

    using TargetManager;

    internal class RollingBoulderBlink : BlinkAbility
    {
        public RollingBoulderBlink(ActiveAbility ability)
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

            var rec = new Polygon.Rectangle(this.Owner.Position, position, this.Ability.Radius);

            if (EntityManager9.Units.Any(x => x.IsHero && x.IsEnemy(this.Owner) && x.IsAlive && rec.IsInside(x.Position)))
            {
                return false;
            }

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