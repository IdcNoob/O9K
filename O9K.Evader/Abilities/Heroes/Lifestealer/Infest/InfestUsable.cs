namespace O9K.Evader.Abilities.Heroes.Lifestealer.Infest
{
    using System.Linq;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class InfestUsable : CounterAbility
    {
        private Unit9 infestTarget;

        public InfestUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            this.infestTarget = EntityManager9.Units
                .Where(
                    x => x.IsUnit && !x.Equals(ally) && x.IsAlive && !x.IsInvulnerable && x.IsVisible && (x.IsAlly(ally) || x.IsCreep)
                         && x.Distance(ally) < this.ActiveAbility.CastRange - 50)
                .OrderBy(x => x.Distance(ally))
                .FirstOrDefault();

            if (this.infestTarget == null)
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetHitTime(this.infestTarget);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.infestTarget.Position);
            return this.ActiveAbility.UseAbility(this.infestTarget, false, true);
        }
    }
}