namespace O9K.Farm.Damage
{
    using Units.Base;

    internal sealed class MeleeDamage : UnitDamage
    {
        public MeleeDamage(UnitDamage damage)
            : base(damage)
        {
            this.HitTime = damage.HitTime + damage.Source.Unit.SecondsPerAttack;
            this.IncludeTime = damage.IncludeTime + damage.Source.Unit.GetAttackBackswing();
        }

        public MeleeDamage(
            FarmUnit source,
            FarmUnit target,
            float attackStartTime,
            float additionalTime,
            float additionalIncludeTime = -0.07f)
            : base(source, target)
        {
            this.HitTime = attackStartTime + source.Unit.GetAttackPoint(target.Unit) + additionalTime;
            this.IncludeTime = this.HitTime + additionalIncludeTime;
        }

        public override float HitTime { get; }

        public override float IncludeTime { get; }
    }
}