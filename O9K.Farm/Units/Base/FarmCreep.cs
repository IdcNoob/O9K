namespace O9K.Farm.Units.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Damage;

    using Ensage.SDK.Helpers;

    using O9K.Core.Entities.Units;
    using O9K.Core.Logger;

    internal class FarmCreep : FarmUnit
    {
        public FarmCreep(Unit9 unit)
            : base(unit)
        {
        }

        public override UnitDamage AddDamage(FarmUnit target, float attackStartTime, bool addNext, bool forceRanged)
        {
            UnitDamage nextDamage;
            var damage = target.IncomingDamage.Find(x => x.IsPredicted && x.Source.Equals(this));
            damage?.Delete();

            if (this.Unit.IsRanged)
            {
                damage = new RangedDamage(this, target, attackStartTime, 138, 0.048f);
                nextDamage = new RangedDamage(damage);
            }
            else
            {
                damage = new MeleeDamage(this, target, attackStartTime, 0.036f);
                nextDamage = new MeleeDamage(damage);
            }

            target.IncomingDamage.Add(damage);

            if (addNext)
            {
                target.IncomingDamage.Add(nextDamage);
            }

            return damage;
        }

        public override void AttackStart(IReadOnlyList<FarmUnit> units, float attackStartTime)
        {
            // ReSharper disable once AsyncVoidFunctionExpression
            UpdateManager.BeginInvoke(
                async () =>
                    {
                        try
                        {
                            var rotation = 0f;

                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            while (rotation != this.Unit.BaseUnit.Rotation)
                            {
                                rotation = this.Unit.BaseUnit.Rotation;
                                await Task.Delay(10);
                            }

                            if (!this.Unit.IsAlive || !this.Unit.IsVisible)
                            {
                                return;
                            }

                            var target = units
                                .Where(
                                    x => x.Unit != this.Unit && x.Unit.IsValid
                                                             && x.Unit.Distance(this.Unit) <= this.Unit.GetAttackRange(x.Unit)
                                                             && !x.Unit.IsAlly(this.Unit))
                                .OrderBy(x => this.Unit.GetAngle(x.Unit.Position))
                                .FirstOrDefault(x => this.Unit.GetAngle(x.Unit.Position) < 0.2f);

                            if (target == null)
                            {
                                return;
                            }

                            this.Target = target;
                            this.AddDamage(target, attackStartTime, true, false);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }
    }
}