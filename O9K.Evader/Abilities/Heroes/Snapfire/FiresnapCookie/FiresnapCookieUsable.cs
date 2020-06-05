namespace O9K.Evader.Abilities.Heroes.Snapfire.FiresnapCookie
{
    using System.Linq;

    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Snapfire;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Managers.Entity;

    using Ensage.SDK.Geometry;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class FiresnapCookieUsable : DisableAbility
    {
        private readonly FiresnapCookie firesnapCookie;

        private Unit9 target;

        public FiresnapCookieUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.firesnapCookie = (FiresnapCookie)ability;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (!this.Ability.CanBeCasted(false))
            {
                return false;
            }

            var castRange = this.ActiveAbility.CastRange;
            var radius = this.ActiveAbility.Radius;
            var jumpRange = this.firesnapCookie.JumpRange;
            var maxRange = this.ActiveAbility.Range;
            var minRange = jumpRange - radius;

            var targets = EntityManager9.Units.Where(
                    x => x.IsUnit && (x.IsMyHero || (!x.IsHero || x.IsIllusion)) && x.IsAlly(this.Owner) && x.IsAlive
                         && x.Distance(enemy) < maxRange && x.Distance(enemy) > minRange && x.Distance(this.Owner) < castRange)
                .OrderBy(x => x.IsMyHero);

            foreach (var unit in targets)
            {
                var delay = this.ActiveAbility.GetCastDelay(unit);
                var targetPosition = unit.GetPredictedPosition(delay - this.ActiveAbility.ActivationDelay);
                var targetJumpPosition = targetPosition.Extend2D(unit.InFront(2000), jumpRange);
                var enemyPosition = enemy.GetPredictedPosition(delay);

                if (targetJumpPosition.Distance2D(enemyPosition) < radius)
                {
                    this.target = unit;
                    return true;
                }
            }

            return false;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(enemy.Position);
            var use = this.ActiveAbility.UseAbility(this.target, false, true);
            if (use)
            {
                enemy.SetExpectedUnitState(this.AppliesUnitState, this.ActiveAbility.GetHitTime(enemy) + 0.3f);
            }

            return use;
        }
    }
}