namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.skywrath_mage_concussive_shot)]
    public class ConcussiveShot : AreaOfEffectAbility, INuke, IDebuff
    {
        public ConcussiveShot(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "launch_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public string DebuffModifierName { get; } = "modifier_skywrath_mage_concussive_shot_slow";

        public override float Radius
        {
            get
            {
                if (this.Owner.GetAbilityById(AbilityId.special_bonus_unique_skywrath_4)?.Level > 0)
                {
                    return 9999999;
                }

                return base.Radius;
            }
        }

        public override bool CanHit(Unit9 target)
        {
            var possibleTarget = EntityManager9.Heroes
                .Where(
                    x => x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.IsAlly(this.Owner)
                         && x.Distance(this.Owner) < this.Radius)
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault();

            if (!target.Equals(possibleTarget))
            {
                return false;
            }

            return base.CanHit(target);
        }

        public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
        {
            var possibleTarget = EntityManager9.Heroes
                .Where(
                    x => x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.IsAlly(this.Owner)
                         && x.Distance(this.Owner) < this.Radius)
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault();

            if (!mainTarget.Equals(possibleTarget))
            {
                return false;
            }

            return base.CanHit(mainTarget, aoeTargets, minCount);
        }
    }
}