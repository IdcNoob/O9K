namespace O9K.Farm.Units.Base
{
    using System.Collections.Generic;
    using System.Linq;

    using O9K.Core.Entities.Units;

    internal class FarmHero : FarmUnit
    {
        public FarmHero(Unit9 unit)
            : base(unit)
        {
            this.IsHero = true;
            this.IsMyHero = unit.IsMyHero;
        }

        public override bool CanBeKilled
        {
            get
            {
                //todo add ?

                if (this.Unit.IsIllusion)
                {
                    return true;
                }

                return false;
            }
        }

        public override void AttackStart(IReadOnlyList<FarmUnit> units, float attackStartTime)
        {
            var target = units
                .Where(
                    x => x.Unit != this.Unit && x.Unit.Distance(this.Unit) <= this.Unit.GetAttackRange(x.Unit)
                                             && (!x.Unit.IsAlly(this.Unit) || x.Unit.HealthPercentage < 50))
                .OrderBy(x => this.Unit.GetAngle(x.Unit.Position, true))
                .FirstOrDefault(x => this.Unit.GetAngle(x.Unit.Position, true) < 0.2f);

            if (target == null)
            {
                return;
            }

            this.Target = target;
            this.AddDamage(target, attackStartTime, false, false);
        }
    }
}