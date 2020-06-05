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

    [AbilityId(AbilityId.phoenix_icarus_dive)]
    public class IcarusDive : LineAbility
    {
        private readonly SpecialData castRangeData;

        private IcarusDiveStop icarusDiveStop;

        public IcarusDive(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "dash_width");
            this.castRangeData = new SpecialData(baseAbility, "dash_length");
            this.DamageData = new SpecialData(baseAbility, "damage_per_second");
        }

        public override bool HasAreaOfEffect { get; } = false;

        public bool IsFlying
        {
            get
            {
                return this.icarusDiveStop.IsUsable;
            }
        }

        public override float Speed { get; } = 1500;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.icarusDiveStop.UseAbility(queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.phoenix_icarus_dive_stop && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(this.icarusDiveStop));
            }

            this.icarusDiveStop = (IcarusDiveStop)EntityManager9.AddAbility(ability);
        }
    }
}