namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
    using System.Collections.Generic;

    using Base;

    using Core.Helpers;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.invoker_ice_wall)]
    public class IceWall : ActiveAbility, IInvokableAbility //, IDebuff
    {
        private readonly SpecialData count;

        private readonly InvokeHelper<IceWall> invokeHelper;

        private readonly SpecialData spacing;

        public IceWall(Ability baseAbility)
            : base(baseAbility)
        {
            this.invokeHelper = new InvokeHelper<IceWall>(this);

            this.spacing = new SpecialData(baseAbility, "wall_element_spacing");
            this.count = new SpecialData(baseAbility, "num_wall_elements");
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

        public string DebuffModifierName { get; } = "modifier_invoker_ice_wall_slow_debuff";

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

        public override float Radius
        {
            get
            {
                return (this.spacing.GetValue(this.Level) * this.count.GetValue(this.Level)) / 2;
            }
        }

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_quas, AbilityId.invoker_quas, AbilityId.invoker_exort };

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
        }

        public bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false)
        {
            return this.invokeHelper.Invoke(currentOrbs, queue, bypass);
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            return this.Invoke(null, false, bypass) && base.UseAbility(queue, bypass);
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);
            this.invokeHelper.SetOwner(owner);
        }
    }
}