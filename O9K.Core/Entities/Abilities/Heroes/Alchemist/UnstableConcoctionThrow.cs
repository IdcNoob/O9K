namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.alchemist_unstable_concoction_throw)]
    public class UnstableConcoctionThrow : RangedAbility, IDisable
    {
        private readonly SpecialData brewTimeData;

        private UnstableConcoction unstableConcoction;

        public UnstableConcoctionThrow(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "midair_explosion_radius");
            this.SpeedData = new SpecialData(baseAbility, "movement_speed");
            this.brewTimeData = new SpecialData(baseAbility, "brew_time");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float BrewTime
        {
            get
            {
                return this.brewTimeData.GetValue(this.Level);
            }
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            if (this.IsUsable)
            {
                return base.CanBeCasted(checkChanneling);
            }

            return this.unstableConcoction.CanBeCasted(checkChanneling);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.unstableConcoction.UseAbility(queue, bypass);
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            if (!this.IsUsable)
            {
                return this.unstableConcoction.UseAbility(queue, bypass);
            }

            return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            if (!this.IsUsable)
            {
                this.unstableConcoction.UseAbility(queue, bypass);
            }

            return base.UseAbility(target, minimumChance, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.alchemist_unstable_concoction && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.unstableConcoction));
            }

            this.unstableConcoction = (UnstableConcoction)EntityManager9.AddAbility(ability);
        }
    }
}