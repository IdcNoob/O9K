namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
    using System;
    using System.Linq;

    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Managers.Entity;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.ancient_apparition_ice_blast)]
    public class IceBlast : CircleAbility
    {
        private readonly SpecialData radiusGrowData;

        private readonly SpecialData radiusMaxData;

        private IceBlastRelease iceBlastRelease;

        public IceBlast(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius_min");
            this.radiusMaxData = new SpecialData(baseAbility, "radius_max");
            this.radiusGrowData = new SpecialData(baseAbility, "radius_grow");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public override float CastRange { get; } = 9999999;

        public float MaxRadius
        {
            get
            {
                return this.radiusMaxData.GetValue(this.Level);
            }
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            if (this.IsUsable)
            {
                return base.CanBeCasted(checkChanneling);
            }

            return this.iceBlastRelease.CanBeCasted(checkChanneling);
        }

        public float GetRadius(Vector3 position)
        {
            var increase = (this.Owner.Distance(position) / this.Speed) * this.radiusGrowData.GetValue(this.Level);
            return Math.Min(this.Radius + increase, this.MaxRadius);
        }

        public float GetReleaseFlyTime(Vector3 position)
        {
            const float MaxTime = 2f;
            var time = this.Owner.Distance(position) / this.iceBlastRelease.Speed;
            return Math.Min(time, MaxTime);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.iceBlastRelease.UseAbility(queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.ancient_apparition_ice_blast_release && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.iceBlastRelease));
            }

            this.iceBlastRelease = (IceBlastRelease)EntityManager9.AddAbility(ability);
        }
    }
}