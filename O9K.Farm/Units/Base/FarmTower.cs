namespace O9K.Farm.Units.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Damage;

    using Ensage.SDK.Helpers;

    using O9K.Core.Entities.Buildings;
    using O9K.Core.Entities.Units;
    using O9K.Core.Logger;

    internal class FarmTower : FarmUnit
    {
        private readonly Tower9 tower;

        public FarmTower(Unit9 unit)
            : base(unit)
        {
            this.tower = (Tower9)unit;
            this.IsTower = true;
        }

        public override bool CanBeKilled
        {
            get
            {
                if (this.IsAlly)
                {
                    return this.Unit.HealthPercentage < 10;
                }

                return true;
            }
        }

        public override UnitDamage AddDamage(FarmUnit target, float attackStartTime, bool addNext, bool forceRanged)
        {
            var damage = new RangedDamage(this, target, attackStartTime, 0, 0.15f, -0.3f);
            target.IncomingDamage.Add(damage);

            return damage;
        }

        public override void AttackStart(IReadOnlyList<FarmUnit> units, float attackStartTime)
        {
            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            var target = units.FirstOrDefault(x => x.Unit.Equals(this.tower.TowerTarget));
                            if (target == null)
                            {
                                return;
                            }

                            this.Target = target;
                            this.AddDamage(target, attackStartTime, false, false);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }
    }
}