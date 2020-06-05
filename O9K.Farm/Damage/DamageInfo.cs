namespace O9K.Farm.Damage
{
    using Units.Base;

    internal class DamageInfo
    {
        public DamageInfo(FarmUnit unit, FarmUnit target)
        {
            this.Unit = unit;
            this.Delay = unit.GetAttackDelay(target);
            this.PredictedHealth = target.GetPredictedHealth(unit, this.Delay);

            if (this.PredictedHealth <= 0)
            {
                this.IsValid = false;
                return;
            }

            this.MinDamage = unit.GetDamage(target);
            this.IsInAttackRange = unit.Unit.Distance(target.Unit) < unit.Unit.GetAttackRange(target.Unit);
        }

        public float Delay { get; }

        public bool IsInAttackRange { get; }

        public bool IsValid { get; } = true;

        public float MinDamage { get; }

        public float PredictedHealth { get; }

        public FarmUnit Unit { get; }
    }
}