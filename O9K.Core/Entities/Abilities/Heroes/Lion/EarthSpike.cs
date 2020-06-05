namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Extensions;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.lion_impale)]
    public class EarthSpike : LineAbility, IDisable, INuke
    {
        public EarthSpike(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "width");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.RangeData = new SpecialData(baseAbility, "length_buffer");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool BreaksLinkens { get; } = true;

        public override float Range
        {
            get
            {
                var range = this.RangeData.GetValue(this.Level);
                return range + this.CastRange;
            }
        }

        public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
        {
            //todo fix prediction range and delete 

            position = this.Owner.Position.Extend2D(position, Math.Min(this.CastRange, this.Owner.Distance(position)));

            var result = this.BaseAbility.UseAbility(position, queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}