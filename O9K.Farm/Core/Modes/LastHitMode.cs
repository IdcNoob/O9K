namespace O9K.Farm.Core.Modes
{
    using System.Collections.Generic;
    using System.Linq;

    using Damage;

    using Menu;

    using O9K.Core.Helpers;

    using Units.Base;

    internal class LastHitMode : BaseMode
    {
        private readonly Dictionary<FarmUnit, Sleeper> towerFarmSleeper = new Dictionary<FarmUnit, Sleeper>();

        private LastHitMenu lastHitMenu;

        public LastHitMode(UnitManager unitManager, MenuManager menuManager)
            : base(unitManager)
        {
            this.lastHitMenu = menuManager.LastHitMenu;
        }

        protected bool Deny(IReadOnlyList<FarmUnit> enemies, IReadOnlyList<FarmUnit> allies, IReadOnlyList<FarmUnit> myUnits)
        {
            foreach (var ally in allies.Where(x => x.CanBeKilled).OrderBy(x => x.GetPredictedDeathTime(enemies)))
            {
                var damages = new List<DamageInfo>();

                foreach (var unit in myUnits)
                {
                    if (unit.Equals(ally) || !unit.CanAttack(ally))
                    {
                        continue;
                    }

                    var damageInfo = new DamageInfo(unit, ally);
                    if (!damageInfo.IsValid)
                    {
                        continue;
                    }

                    damages.Add(damageInfo);
                }

                if (damages.Count == 0)
                {
                    continue;
                }

                var attack = new List<DamageInfo>();
                var canKill = false;
                var damage = 0f;

                foreach (var damageInfo in damages.OrderBy(x => x.Delay))
                {
                    damage += damageInfo.MinDamage;
                    attack.Add(damageInfo);

                    if (damage < damageInfo.PredictedHealth)
                    {
                        continue;
                    }

                    canKill = true;
                    break;
                }

                if (!canKill)
                {
                    continue;
                }

                foreach (var damageInfo in attack.OrderByDescending(x => x.IsInAttackRange).ThenByDescending(x => x.Delay))
                {
                    //todo force attack if target will die soon

                    if (damageInfo.Unit.Farm(ally))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected IReadOnlyList<FarmUnit> GetAvailableUnitsForDeny(
            IReadOnlyList<FarmUnit> enemies,
            IReadOnlyList<FarmUnit> allies,
            IReadOnlyList<FarmUnit> myUnits)
        {
            var list = new List<FarmUnit>(myUnits.Count);
            var checkUnits = new List<FarmUnit>(myUnits.Count);

            foreach (var farmUnit in myUnits)
            {
                if (farmUnit.IsLastHitEnabled)
                {
                    checkUnits.Add(farmUnit);
                }
                else
                {
                    list.Add(farmUnit);
                }
            }

            foreach (var unit in checkUnits)
            {
                var available = true;

                foreach (var enemy in enemies.Where(x => x.CanBeKilled))
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

        protected IReadOnlyList<FarmUnit> GetAvailableUnitsForHarass(
            IReadOnlyList<FarmUnit> enemies,
            IReadOnlyList<FarmUnit> allies,
            IReadOnlyList<FarmUnit> myUnits)
        {
            var list = new List<FarmUnit>(myUnits.Count);

            foreach (var unit in myUnits)
            {
                if (!unit.IsHarassEnabled)
                {
                    continue;
                }

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

        protected bool LastHit(IReadOnlyList<FarmUnit> enemies, IReadOnlyList<FarmUnit> allies, IReadOnlyList<FarmUnit> myUnits)
        {
            foreach (var enemy in enemies.Where(x => x.CanBeKilled).OrderBy(x => x.GetPredictedDeathTime(allies)))
            {
                var damages = new List<DamageInfo>();

                foreach (var unit in myUnits)
                {
                    if (!unit.CanAttack(enemy))
                    {
                        continue;
                    }

                    var damageInfo = new DamageInfo(unit, enemy);
                    if (!damageInfo.IsValid)
                    {
                        continue;
                    }

                    damages.Add(damageInfo);
                }

                if (damages.Count == 0)
                {
                    continue;
                }

                var attack = new List<DamageInfo>();
                var canKill = false;
                var damage = 0f;

                foreach (var damageInfo in damages.OrderBy(x => x.Delay))
                {
                    damage += damageInfo.MinDamage;
                    attack.Add(damageInfo);

                    if (damage < damageInfo.PredictedHealth)
                    {
                        continue;
                    }

                    canKill = true;
                    break;
                }

                if (!canKill)
                {
                    continue;
                }

                foreach (var damageInfo in attack.OrderByDescending(x => x.IsInAttackRange).ThenByDescending(x => x.Delay))
                {
                    //todo force attack if target will die soon

                    if (damageInfo.Unit.Farm(enemy))
                    {
                        return true;
                    }
                }
            }

            return false;
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

            if (this.TowerPrepare(enemies, allies, availableUnits.Where(x => x.IsLastHitEnabled).ToList()))
            {
                return;
            }

            if (this.Deny(enemies, allies, availableUnits.Where(x => x.IsDenyEnabled).ToList()))
            {
                return;
            }

            availableUnits = this.GetAvailableUnitsForHarass(enemies, allies, availableUnits);

            if (this.Harass(enemies, allies, availableUnits))
            {
                return;
            }

            this.MoveToMouse(myUnits);
        }

        private bool Harass(IReadOnlyList<FarmUnit> enemies, IReadOnlyList<FarmUnit> allies, IReadOnlyList<FarmUnit> myUnits)
        {
            foreach (var enemy in enemies.Where(x => x.IsHero && !x.Unit.IsIllusion))
            {
                foreach (var unit in myUnits)
                {
                    if (!unit.CanAttack(enemy, 0))
                    {
                        continue;
                    }

                    if (enemies.Any(x => x.IsTower && x.Unit.Distance(unit.Unit) < 500))
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

        private bool TowerPrepare(IReadOnlyList<FarmUnit> enemies, IReadOnlyList<FarmUnit> allies, IReadOnlyList<FarmUnit> myUnits)
        {
            foreach (var tower in allies.Where(x => x.IsTower && x.Target?.Unit.IsValid == true && x.Target.Unit.IsAlive))
            {
                var target = tower.Target;
                if (target.IsHero)
                {
                    continue;
                }

                if (this.towerFarmSleeper.TryGetValue(target, out var sleeper) && sleeper.IsSleeping)
                {
                    continue;
                }

                if (allies.Any(x => !x.IsControllable && !x.IsTower && x.Target?.Equals(target) == true))
                {
                    continue;
                }

                foreach (var unit in myUnits)
                {
                    if (!unit.CanAttack(target, 0))
                    {
                        continue;
                    }

                    var myDamage = unit.GetDamage(target);
                    var towerDamage = tower.GetAverageDamage(target);

                    if (target.Unit.Health % towerDamage > myDamage)
                    {
                        if (unit.Harass(target))
                        {
                            var sleep = new Sleeper();
                            sleep.Sleep(unit.GetAttackDelay(target) + 0.2f);
                            this.towerFarmSleeper[target] = sleep;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}