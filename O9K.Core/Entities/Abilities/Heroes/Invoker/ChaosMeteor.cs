namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Core.Helpers;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.invoker_chaos_meteor)]
    public class ChaosMeteor : LineAbility, IInvokableAbility, IHarass
    {
        private readonly InvokeHelper<ChaosMeteor> invokeHelper;

        public ChaosMeteor(Ability baseAbility)
            : base(baseAbility)
        {
            this.invokeHelper = new InvokeHelper<ChaosMeteor>(this);

            this.ActivationDelayData = new SpecialData(baseAbility, "land_time");
            this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
            this.RangeData = new SpecialData(baseAbility, "travel_distance");
            this.SpeedData = new SpecialData(baseAbility, "travel_speed");
        }

        public bool CanBeInvoked
        {
            get
            {
                if (this.IsInvoked)
                {
                    return true;
                }

                return this.invokeHelper.CanInvoke(false);
            }
        }

        public bool IsInvoked
        {
            get
            {
                return this.invokeHelper.IsInvoked;
            }
        }

        public override bool IsUsable
        {
            get
            {
                if (!this.IsAvailable)
                {
                    return false;
                }

                return true;
            }
        }

        public override float Range
        {
            get
            {
                var range = this.RangeData.GetValue(this.invokeHelper.Wex.Level);
                return range + this.Radius;
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_exort, AbilityId.invoker_exort, AbilityId.invoker_wex };

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
        }

        public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
        {
            return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
        }

        public override bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
        {
            return this.Invoke(null, false, bypass) && base.UseAbility(position, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            this.invokeHelper.SetOwner(owner);
        }
    }
}