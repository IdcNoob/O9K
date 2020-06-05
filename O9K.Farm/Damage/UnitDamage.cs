namespace O9K.Farm.Damage
{
    using System;

    using O9K.Core.Helpers.Damage;

    using Units.Base;

    internal abstract class UnitDamage
    {
        private int averageDamage;

        private int maxDamage;

        private int minDamage;

        protected UnitDamage(UnitDamage damage)
        {
            this.Source = damage.Source;
            this.Target = damage.Target;
            this.MinDamage = damage.MinDamage;
            this.MaxDamage = damage.MaxDamage;
            this.AverageDamage = damage.AverageDamage;
            this.IsPredicted = true;
        }

        protected UnitDamage(FarmUnit source, FarmUnit target)
        {
            this.Source = source;
            this.Target = target;
            this.MinDamage = source.Unit.GetAttackDamage(target.Unit, DamageValue.Minimum);
            this.MaxDamage = source.Unit.GetAttackDamage(target.Unit, DamageValue.Maximum);
            this.AverageDamage = (this.MinDamage + this.MaxDamage) / 2;
        }

        public int AverageDamage
        {
            get
            {
                return this.averageDamage;
            }
            protected set
            {
                this.averageDamage = Math.Max(0, value);
            }
        }

        public abstract float HitTime { get; }

        public abstract float IncludeTime { get; }

        public bool IsPredicted { get; protected set; }

        public int MaxDamage
        {
            get
            {
                return this.maxDamage;
            }
            protected set
            {
                this.maxDamage = Math.Max(0, value);
            }
        }

        public int MinDamage
        {
            get
            {
                return this.minDamage;
            }
            protected set
            {
                this.minDamage = Math.Max(0, value);
            }
        }

        public FarmUnit Source { get; }

        public FarmUnit Target { get; }

        public void Delete()
        {
            this.minDamage = 0;
            this.maxDamage = 0;
            this.averageDamage = 0;
            this.IsPredicted = false;
        }

        public bool WillHit(float min, float max)
        {
            if (min > this.IncludeTime)
            {
                return false;
            }

            return this.HitTime > min && this.HitTime < max;
        }
    }
}