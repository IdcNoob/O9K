namespace O9K.Farm.Core.Modes
{
    using System.Collections.Generic;
    using System.Linq;

    using Damage;

    using Menu;

    using Units.Base;

    internal class PushMode : LastHitMode
    {
        public PushMode(UnitManager unitManager, MenuManager menuManager)
            : base(unitManager, menuManager)
        {
        }

        protected override void OnUpdate(IReadOnlyList<FarmUnit> units, IReadOnlyList<FarmUnit> myUnits)
        {
            var allies = units.Where(x => x.IsAlly).ToList();
            var enemies = units.Where(x => !x.IsAlly).ToList();

            if (this.LastHit(enemies, allies, myUnits.Where(x => x.IsLastHitEnabled).ToList()))
            {
                return;
            }

            var availableUnits = this.GetAvailableUnitsForDeny(enemies, allies, myUnits);

            if (this.Deny(enemies, allies, availableUnits.Where(x => x.IsDenyEnabled).ToList()))
            {
                return;
            }

            availableUnits = this.GetAvailableUnitsForPush(enemies, allies, availableUnits);

            if (this.Push(enemies, allies, availableUnits))
            {
                return;
            }

            this.MoveToMouse(myUnits);
        }

        private IReadOnlyList<FarmUnit> GetAvailableUnitsForPush(
            List<FarmUnit> enemies,
            List<FarmUnit> allies,
            IReadOnlyList<FarmUnit> myUnits)
        {
            var list = new List<FarmUnit>(myUnits.Count);

            foreach (var unit in myUnits)
            {
                var available = true;

                foreach (var enemy in allies.Where(x => x.CanBeKilled))
                {
                    var damageInfo = new DamageInfo(unit, enemy);
                    if (!damageInfo.IsValid || !damageInfo.IsInAttackRange)
                    {
                        continue;
                    }

                    if (damageInfo.PredictedHealth > damageInfo.MinDamage * 2
                        && enemy.GetPredictedHealth(unit, unit.Unit.SecondsPerAttack + 1) > damageInfo.MinDamage)
                    {
                        continue;
                    }

                    available = false;
                    break;
                }

                if (available)
                {
                    list.Add(unit);
                }
            }

            return list;
        }

        private bool Push(List<FarmUnit> enemies, List<FarmUnit> allies, IReadOnlyList<FarmUnit> myUnits)
        {
            foreach (var unit in myUnits)
            {
                foreach (var enemy in enemies.Where(x => !x.IsHero).OrderBy(x => x.Unit.Distance(unit.Unit)))
                {
                    if (!unit.CanAttack(enemy))
                    {
                        continue;
                    }

                    var damage = unit.GetAverageDamage(enemy);
                    var health = enemy.GetPredictedHealth(unit, unit.GetAttackDelay(enemy));

                    if (health - damage < damage && enemies.Any(x => x.Target?.Equals(enemy) == true))
                    {
                        continue;
                    }

                    if (enemy.GetPredictedHealth(unit, unit.Unit.SecondsPerAttack + 1) < damage)
                    {
                        continue;
                    }

                    if (unit.Harass(enemy))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}