namespace O9K.Farm.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage;

    using Menu;

    using O9K.Core.Entities.Metadata;
    using O9K.Core.Entities.Units;
    using O9K.Core.Logger;
    using O9K.Core.Managers.Entity;

    using Units.Base;

    internal class UnitManager : IDisposable
    {
        private readonly MenuManager menuManager;

        private readonly List<FarmUnit> units = new List<FarmUnit>();

        private readonly Dictionary<string, Type> unitTypes = new Dictionary<string, Type>();

        //private int deny = ObjectManager.LocalPlayer.DenyCount;

        //private int lastHit = ObjectManager.LocalPlayer.LastHitCount;

        //private int missedDeny;

        //private int missedLastHit;

        public UnitManager(MenuManager menuManager)
        {
            this.menuManager = menuManager;

            foreach (var type in Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && typeof(FarmUnit).IsAssignableFrom(x)))
            {
                foreach (var attribute in type.GetCustomAttributes<UnitNameAttribute>())
                {
                    this.unitTypes.Add(attribute.Name, type);
                }
            }

            EntityManager9.UnitAdded += this.OnUnitAdded;
            EntityManager9.UnitRemoved += this.OnUnitRemoved;
            EntityManager9.UnitMonitor.AttackEnd += this.OnAttackEnd;
            //     EntityManager9.UnitMonitor.UnitDied += OnUnitDied;

            //     Drawing.OnDraw += DrawingOnOnDraw;
            //     EntityManager9.UnitMonitor.UnitDied += UnitMonitorOnUnitDied;
        }

        public IEnumerable<FarmUnit> Units
        {
            get
            {
                return this.units.Where(x => x.IsValid);
            }
        }

        public void Dispose()
        {
            EntityManager9.UnitAdded -= this.OnUnitAdded;
            EntityManager9.UnitRemoved -= this.OnUnitRemoved;
            EntityManager9.UnitMonitor.AttackEnd -= this.OnAttackEnd;
            EntityManager9.UnitMonitor.UnitDied -= this.OnUnitDied;
        }

        public FarmUnit GetControllableUnit(Unit9 unit)
        {
            return this.units.Find(x => x.IsControllable && x.Unit == unit && x.Unit.IsAlive);
        }

        public FarmUnit GetUnit(Entity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return this.units.Find(x => x.Unit.Handle == entity.Handle);
        }

        //        private void DrawingOnOnDraw(EventArgs args)
        //        {

        //            Drawing.DrawText("LH: " + this.missedLastHit, "Arial", new Vector2(10, 110), new Vector2(20), Color.White, FontFlags.None);
        //            Drawing.DrawText("DN: " + this.missedDeny, "Arial", new Vector2(10, 130), new Vector2(20), Color.White, FontFlags.None);

        //            var myHero = this.Units.FirstOrDefault(x => x.IsControllable && EntityManager9.Owner.SelectedUnits.Contains(x.Unit));
        //            if (myHero != null)
        //            {
        //                Drawing.DrawText(
        //                    myHero.AttackSleeper.IsSleeping + " " + myHero.MoveSleeper.IsSleeping,
        //                    "Arial",
        //                    myHero.Unit.HealthBarPosition + new Vector2(20, 20),
        //                    new Vector2(20),
        //                    Color.White,
        //                    FontFlags.None);
        //            }

        //            foreach (var unit in this.Units)
        //            {
        //                var position = unit.Unit.HealthBarPosition - new Vector2(40, 20);

        //                foreach (var damage in unit.IncomingDamage)
        //                {
        //                    if (damage.MinDamage <= 0 || Game.RawGameTime > damage.IncludeTime)
        //                    {
        //                        continue;
        //                    }

        //                    var text = damage.MinDamage.ToString();

        //                    if (damage is RangedDamage ranged && ranged.Projectile != null)
        //                    {
        //                        text += "!";
        //                    }

        //                    if (damage.IsPredicted)
        //                    {
        //                        text += "*";
        //                    }

        //                    if (damage.Source.IsControllable)
        //                    {
        //                        text += "^";
        //                    }

        //                    Drawing.DrawText(text, "Arial", position += new Vector2(0, 20), new Vector2(20), Color.White, FontFlags.None);
        //                }

        //                if (myHero == null || unit == myHero)
        //                {
        //                    continue;
        //                }

        //                Drawing.DrawText(
        //                    unit.Unit.Health + " " + unit.GetPredictedHealth(myHero, myHero.GetAttackDelay(unit)),
        //                    "Arial",
        //                    unit.Unit.HealthBarPosition + new Vector2(20, 0),
        //                    new Vector2(20),
        //                    Color.White,
        //                    FontFlags.None);
        //            }
        //        }

        private void OnAttackEnd(Unit9 unit)
        {
            var controllable = this.GetControllableUnit(unit);
            if (controllable == null)
            {
                return;
            }

            controllable.AttackSleeper.Reset();
            controllable.MoveSleeper.Reset();
        }

        private void OnUnitAdded(Unit9 unit)
        {
            try
            {
                if (!unit.IsUnit)
                {
                    return;
                }

                FarmUnit farmUnit;

                if (this.unitTypes.TryGetValue(unit.Name, out var type))
                {
                    farmUnit = (FarmUnit)Activator.CreateInstance(type, unit);
                }
                else if (unit.IsLaneCreep)
                {
                    farmUnit = new FarmCreep(unit);
                }
                else if (unit.IsHero)
                {
                    farmUnit = new FarmHero(unit);
                }
                else if (unit.IsTower)
                {
                    farmUnit = new FarmTower(unit);
                }
                else
                {
                    farmUnit = new FarmUnit(unit);
                }

                if (unit.IsMyControllable && unit.AttackCapability != AttackCapability.None
                                          && (unit.UnitState & UnitState.CommandRestricted) == 0)
                {
                    farmUnit.CreateMenu(this.menuManager.UnitSettingsMenu);
                }

                this.units.Add(farmUnit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitDied(Unit9 unit)
        {
            foreach (var farmUnit in this.Units.Where(x => x.IsControllable && x.Target?.Unit.Equals(unit) == true))
            {
                farmUnit.AttackSleeper.Reset();
                farmUnit.MoveSleeper.Reset();
            }
        }

        private void OnUnitRemoved(Unit9 unit)
        {
            try
            {
                var farmUnit = this.units.Find(x => x.Unit == unit);
                if (farmUnit == null)
                {
                    return;
                }

                this.units.Remove(farmUnit);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        //private void UnitMonitorOnUnitDied(Unit9 unit)
        //{
        //    if (!unit.IsCreep)
        //    {
        //        return;
        //    }

        //    UpdateManager.BeginInvoke(
        //        () =>
        //            {
        //                var owner = EntityManager9.Owner;
        //                if (unit.Distance(owner) > 1500)
        //                {
        //                    return;
        //                }

        //                if (unit.IsAlly())
        //                {
        //                    if (owner.Player.DenyCount != this.deny + 1)
        //                    {
        //                        this.missedDeny++;
        //                    }

        //                    this.deny = owner.Player.DenyCount;
        //                }
        //                else
        //                {
        //                    if (owner.Player.LastHitCount != this.lastHit + 1)
        //                    {
        //                        this.missedLastHit++;
        //                    }

        //                    this.lastHit = owner.Player.LastHitCount;
        //                }
        //            },
        //        100);
        //}
    }
}