namespace O9K.Evader.Abilities.Heroes.Terrorblade.Sunder
{
    using System.Linq;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class SunderUsable : CounterAbility
    {
        private Unit9 target;

        public SunderUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            var health = ally.Health;
            var damage = obstacle.GetDamage(ally);
            if (damage < health)
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var damagePercentage = (obstacle.GetDamage(ally) / ally.MaximumHealth) * 100;
            var remainingTime = obstacle.GetEvadeTime(ally, false);

            var possibleTargets = EntityManager9.Units.Where(
                    x => x.IsHero && x.IsAlive && !x.IsInvulnerable && x.IsVisible && x.HealthPercentage > damagePercentage + 5
                         && !x.IsLinkensProtected && !x.IsLotusProtected && !x.IsUntargetable && this.ActiveAbility.CanHit(x)
                         && this.ActiveAbility.GetHitTime(x) < remainingTime)
                .OrderByDescending(x => x.HealthPercentage)
                .ToList();

            this.target = possibleTargets.Find(x => x.IsEnemy(ally) && !x.IsIllusion);

            if (this.target == null)
            {
                this.target = possibleTargets.Find(x => x.IsIllusion);
            }

            if (this.target == null)
            {
                this.target = possibleTargets.FirstOrDefault();
            }

            if (this.target == null)
            {
                return 99999;
            }

            return this.ActiveAbility.GetHitTime(this.target);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.target.Position);
            return this.ActiveAbility.UseAbility(this.target, false, true);
        }
    }
}