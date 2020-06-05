namespace O9K.Core.Entities.Abilities.Heroes.Tusk
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

    [AbilityId(AbilityId.tusk_snowball)]
    public class Snowball : RangedAbility, IDisable
    {
        private LaunchSnowball launch;

        public Snowball(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "snowball_damage");
            //this.SpeedData = new SpecialData(baseAbility, "snowball_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            if (this.IsHidden)
            {
                return this.launch.CanBeCasted(checkChanneling);
            }

            return base.CanBeCasted(checkChanneling);
        }

        public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
        {
            if (this.IsHidden)
            {
                return this.launch.UseAbility(queue, bypass);
            }

            return base.UseAbility(target, queue, bypass);
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            if (this.IsHidden)
            {
                return this.launch.UseAbility(queue, bypass);
            }

            return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            if (this.IsHidden)
            {
                return this.launch.UseAbility(queue, bypass);
            }

            return base.UseAbility(target, minimumChance, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.tusk_launch_snowball && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.launch));
            }

            this.launch = (LaunchSnowball)EntityManager9.AddAbility(ability);
        }
    }
}