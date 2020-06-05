namespace O9K.Core.Entities.Abilities.Heroes.Tinker
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

    [AbilityId(AbilityId.tinker_heat_seeking_missile)]
    public class HeatSeekingMissile : AreaOfEffectAbility, INuke
    {
        private readonly SpecialData scpeterTargetsData;

        private readonly SpecialData targetsData;

        public HeatSeekingMissile(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.scpeterTargetsData = new SpecialData(baseAbility, "targets_scepter");
            this.targetsData = new SpecialData(baseAbility, "targets");
        }

        public override bool CanHit(Unit9 target)
        {
            var hitCount = (int)(this.Owner.HasAghanimsScepter
                                     ? this.scpeterTargetsData.GetValue(this.Level)
                                     : this.targetsData.GetValue(this.Level));

            var possibleTargets = EntityManager9.Units
                .Where(
                    x => x.IsHero && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable
                         && x.Distance(this.Owner) < this.Radius)
                .OrderBy(x => x.Distance(this.Owner))
                .Take(hitCount);

            if (!possibleTargets.Contains(target))
            {
                return false;
            }

            return base.CanHit(target);
        }

        public override bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
        {
            var hitCount = (int)(this.Owner.HasAghanimsScepter
                                     ? this.scpeterTargetsData.GetValue(this.Level)
                                     : this.targetsData.GetValue(this.Level));

            var possibleTargets = EntityManager9.Units
                .Where(
                    x => x.IsHero && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable
                         && x.Distance(this.Owner) < this.Radius)
                .OrderBy(x => x.Distance(this.Owner))
                .Take(hitCount);

            if (!possibleTargets.Contains(mainTarget))
            {
                return false;
            }

            return base.CanHit(mainTarget, aoeTargets, minCount);
        }
    }
}