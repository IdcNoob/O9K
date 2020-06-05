namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
    using System;
    using System.Linq;

    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.phoenix_sun_ray)]
    public class SunRay : LineAbility
    {
        private SunRayStop sunRayStop;

        private SunRayToggleMovement sunRayToggleMovement;

        public SunRay(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "base_damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public override bool HasAreaOfEffect { get; } = false;

        public bool IsSunRayActive
        {
            get
            {
                return this.IsHidden;
            }
        }

        public bool IsSunRayMoving { get; private set; }

        public bool Stop()
        {
            return this.sunRayStop.UseAbility();
        }

        public bool ToggleMovement()
        {
            if (this.sunRayToggleMovement.UseAbility())
            {
                this.IsSunRayMoving = !this.IsSunRayMoving;
                return true;
            }

            return false;
        }

        public override bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            this.IsSunRayMoving = false;
            return base.UseAbility(target, minimumChance, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.phoenix_sun_ray_stop && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.sunRayStop));
            }

            this.sunRayStop = (SunRayStop)EntityManager9.AddAbility(ability);

            ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.phoenix_sun_ray_toggle_move && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.sunRayToggleMovement));
            }

            this.sunRayToggleMovement = (SunRayToggleMovement)EntityManager9.AddAbility(ability);
        }
    }
}