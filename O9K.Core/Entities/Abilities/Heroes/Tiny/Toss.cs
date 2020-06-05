namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.tiny_toss)]
    public class Toss : RangedAbility, INuke
    {
        private bool talentLearned;

        public Toss(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "toss_damage");
            this.RadiusData = new SpecialData(baseAbility, "grab_radius");
        }

        public override bool BreaksLinkens { get; } = false;

        public override bool CanHitSpellImmuneEnemy { get; } = false;

        public override bool IsDisplayingCharges
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_tiny_2)?.Level > 0;
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

            var grabTarget = EntityManager9.Units
                .Where(
                    x => x.IsUnit && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.Equals(this.Owner)
                         && x.Distance(this.Owner) < this.RadiusData.GetValue(this.Level))
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault();

            if (grabTarget == null)
            {
                return false;
            }

            if (grabTarget.IsHero && !grabTarget.IsIllusion && grabTarget.IsAlly(this.Owner))
            {
                return false;
            }

            return true;
        }
    }
}