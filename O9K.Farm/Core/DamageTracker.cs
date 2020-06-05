namespace O9K.Farm.Core
{
    using System;
    using System.Linq;

    using Damage;

    using Ensage;

    using O9K.Core.Entities.Units;
    using O9K.Core.Extensions;
    using O9K.Core.Logger;
    using O9K.Core.Managers.Entity;

    internal class DamageTracker : IDisposable
    {
        private readonly UnitManager unitManager;

        public DamageTracker(UnitManager unitManager)
        {
            this.unitManager = unitManager;

            EntityManager9.UnitMonitor.AttackStart += this.OnAttackStart;
            EntityManager9.UnitMonitor.AttackEnd += this.OnUnitDied;
            EntityManager9.UnitMonitor.UnitDied += this.OnUnitDied;
            ObjectManager.OnAddTrackingProjectile += this.OnAddTrackingProjectile;
        }

        public event EventHandler<UnitDamage> AttackCanceled;

        public void Dispose()
        {
            EntityManager9.UnitMonitor.AttackStart -= this.OnAttackStart;
            EntityManager9.UnitMonitor.AttackEnd -= this.OnUnitDied;
            EntityManager9.UnitMonitor.UnitDied -= this.OnUnitDied;
            ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
        }

        private void OnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            try
            {
                var projectile = args.Projectile;
                if (!projectile.IsAutoAttackProjectile())
                {
                    return;
                }

                var source = this.unitManager.GetUnit(projectile.Source);
                if (source?.IsControllable != false)
                {
                    return;
                }

                var target = projectile.Target;
                if (target == null)
                {
                    return;
                }

                var allUnits = this.unitManager.Units.ToList();
                var damages = allUnits.SelectMany(x => x.IncomingDamage).OfType<RangedDamage>().ToList();
                var damage = damages.LastOrDefault(
                    x => x.Projectile == null && !x.IsPredicted && x.MinDamage > 0 && x.Source.Unit.Handle == source.Unit.Handle
                         && x.Target.Unit.Handle == target.Handle);

                if (damage != null)
                {
                    if (Game.RawGameTime < damage.HitTime)
                    {
                        damage.AddProjectile(projectile);
                    }
                }
                else
                {
                    damage = damages.Find(x => x.Projectile == null && x.Source.Unit.Handle == source.Unit.Handle);

                    if (damage != null)
                    {
                        //wrong projectile target
                        damage.Delete();
                        this.AttackCanceled?.Invoke(this, damage);
                    }

                    var newTarget = allUnits.Find(x => x.Unit.Handle == target.Handle);
                    if (newTarget == null)
                    {
                        return;
                    }

                    damage = (RangedDamage)source.AddDamage(newTarget, 0, false, true);
                    damage.AddProjectile(projectile);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnAttackStart(Unit9 unit)
        {
            try
            {
                var units = this.unitManager.Units.ToList();
                var farmUnit = units.Find(x => x.Unit == unit);
                if (farmUnit == null)
                {
                    return;
                }

                var time = Game.RawGameTime - (Game.Ping / 2000);
                if (farmUnit.IsControllable)
                {
                    farmUnit.AttackStartTime = time;
                    return;
                }

                farmUnit.AttackStart(units, time);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUnitDied(Unit9 unit)
        {
            try
            {
                foreach (var farmUnit in this.unitManager.Units)
                {
                    foreach (var damage in farmUnit.IncomingDamage)
                    {
                        if (damage.MinDamage <= 0 || damage.Source.Unit != unit || damage.HitTime <= Game.RawGameTime)
                        {
                            continue;
                        }

                        if (damage is RangedDamage ranged && ranged.Projectile != null)
                        {
                            continue;
                        }

                        damage.Delete();
                        this.AttackCanceled?.Invoke(this, damage);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}