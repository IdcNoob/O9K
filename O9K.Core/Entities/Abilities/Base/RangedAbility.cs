namespace O9K.Core.Entities.Abilities.Base
{
    using System.Collections.Generic;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Prediction.Data;

    public abstract class RangedAbility : ActiveAbility
    {
        protected SpecialData RangeData;

        protected RangedAbility(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float CastRange
        {
            get
            {
                var range = this.BaseCastRange + this.Owner.BonusCastRange;

                if (this.UnitTargetCast)
                {
                    range += 50;
                }

                return range;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.BaseAbility.CastRange;
            }
        }

        public override bool CanHit(Unit9 target)
        {
            if (target.IsMagicImmune && ((target.IsEnemy(this.Owner) && !this.CanHitSpellImmuneEnemy)
                                         || (target.IsAlly(this.Owner) && !this.CanHitSpellImmuneAlly)))
            {
                return false;
            }

            if (this.Owner.Distance(target) > this.CastRange)
            {
                return false;
            }

            return true;
        }

        public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
        {
            return this.CanHit(mainTarget);
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            return this.UseAbility(target, queue, bypass);
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            return this.UseAbility(mainTarget, queue, bypass);
        }
    }
}