namespace O9K.Core.Entities.Abilities.Base
{
    using Ensage;

    using Entities.Units;

    public abstract class OrbAbility : AutoCastAbility
    {
        protected OrbAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float CastPoint
        {
            get
            {
                return this.Owner.GetAttackPoint();
            }
        }

        public override float CastRange
        {
            get
            {
                return this.BaseCastRange + this.Owner.BonusAttackRange;
            }
        }

        public virtual string OrbModifier { get; } = string.Empty;

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed;
            }
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.Owner.CanAttack() && base.CanBeCasted(checkChanneling);
        }

        public override bool CanHit(Unit9 target)
        {
            if (target.IsMagicImmune && !this.CanHitSpellImmuneEnemy)
            {
                return false;
            }

            if (target.IsAttackImmune)
            {
                return false;
            }

            if (target.IsAlly(this.Owner))
            {
                return false;
            }

            if (this.Owner.Distance(target) > this.CastRange)
            {
                return false;
            }

            return true;
        }
    }
}