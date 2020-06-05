namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.invoker_cold_snap)]
    public class ColdSnap : RangedAbility, IInvokableAbility, IDisable
    {
        private readonly InvokeHelper<ColdSnap> invokeHelper;

        public ColdSnap(Ability baseAbility)
            : base(baseAbility)
        {
            this.invokeHelper = new InvokeHelper<ColdSnap>(this);
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

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

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_quas, AbilityId.invoker_quas };

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
        }

        public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
        {
            return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
        }

        public override bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
        {
            return this.Invoke(null, false, bypass) && base.UseAbility(target, queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            this.invokeHelper.SetOwner(owner);
        }
    }
}