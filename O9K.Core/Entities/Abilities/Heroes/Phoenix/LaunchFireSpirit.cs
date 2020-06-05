namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
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

    [AbilityId(AbilityId.phoenix_launch_fire_spirit)]
    public class LaunchFireSpirit : CircleAbility, IDebuff
    {
        private FireSpirits fireSpirits;

        public LaunchFireSpirit(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "spirit_speed");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_phoenix_fire_spirit_burn";

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            if (this.IsUsable)
            {
                return base.CanBeCasted(checkChanneling);
            }

            return this.fireSpirits.CanBeCasted(checkChanneling);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.fireSpirits.UseAbility(queue, bypass);
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
                return this.fireSpirits.UseAbility(queue, bypass);
            }

            return base.UseAbility(mainTarget, aoeTargets, minimumChance, minAOETargets, queue, bypass);
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            if (!this.IsUsable)
            {
                this.fireSpirits.UseAbility(queue, bypass);
            }

            return base.UseAbility(target, minimumChance, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.phoenix_fire_spirits && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.fireSpirits));
            }

            this.fireSpirits = (FireSpirits)EntityManager9.AddAbility(ability);
        }
    }
}