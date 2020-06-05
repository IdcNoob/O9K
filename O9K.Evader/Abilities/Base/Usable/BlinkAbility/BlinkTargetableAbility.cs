namespace O9K.Evader.Abilities.Base.Usable.BlinkAbility
{
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BlinkTargetableAbility : BlinkAbility
    {
        private Unit9 blinkTarget;

        public BlinkTargetableAbility(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            //todo check if target is in obstacle
            this.blinkTarget = EntityManager9.Units
                .Where(
                    x => x.IsUnit && !x.Equals(this.Owner) && x.IsAlive && !x.IsInvulnerable && x.IsAlly(ally)
                         && x.Distance(ally) < this.ActiveAbility.CastRange && x.Distance(ally) > this.ActiveAbility.CastRange / 3)
                .OrderBy(x => x.Distance(this.FountainPosition))
                .FirstOrDefault();

            if (this.blinkTarget == null)
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var additionalTime = 0f;
            if (this.ActiveAbility.Speed > 0)
            {
                additionalTime = 0.15f;
            }

            return this.ActiveAbility.GetCastDelay(this.blinkTarget.Position) + additionalTime;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(this.blinkTarget.Position);
            return this.ActiveAbility.UseAbility(this.blinkTarget, false, true);
        }
    }
}