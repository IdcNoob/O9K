namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Core.Helpers;
    using Core.Helpers.Damage;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.invoker_emp)]
    // ReSharper disable once InconsistentNaming
    public class EMP : CircleAbility, IInvokableAbility, INuke
    {
        private readonly SpecialData damagePctData;

        private readonly InvokeHelper<EMP> invokeHelper;

        public EMP(Ability baseAbility)
            : base(baseAbility)
        {
            this.invokeHelper = new InvokeHelper<EMP>(this);

            this.ActivationDelayData = new SpecialData(baseAbility, "delay");
            this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
            this.DamageData = new SpecialData(baseAbility, "mana_burned");
            this.damagePctData = new SpecialData(baseAbility, "damage_per_mana_pct");
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

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_wex, AbilityId.invoker_wex, AbilityId.invoker_wex };

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var manaBurn = this.DamageData.GetValue(this.invokeHelper.Wex.Level);
            var manaDamagePercentage = this.damagePctData.GetValue(this.Level) / 100;

            return new Damage
            {
                [this.DamageType] = (int)(Math.Min(unit.Mana, manaBurn) * manaDamagePercentage)
            };
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