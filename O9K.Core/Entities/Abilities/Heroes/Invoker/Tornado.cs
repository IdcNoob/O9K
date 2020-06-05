namespace O9K.Core.Entities.Abilities.Heroes.Invoker
{
    using System;
    using System.Collections.Generic;

    using Base;
    using Base.Components;
    using Base.Types;

    using Core.Helpers;
    using Core.Helpers.Damage;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.invoker_tornado)]
    public class Tornado : LineAbility, IInvokableAbility, IDisable, INuke, IAppliesImmobility
    {
        private readonly InvokeHelper<Tornado> invokeHelper;

        public Tornado(Ability baseAbility)
            : base(baseAbility)
        {
            this.invokeHelper = new InvokeHelper<Tornado>(this);

            this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
            this.DamageData = new SpecialData(baseAbility, "wex_damage");
            this.RangeData = new SpecialData(baseAbility, "travel_distance");
            this.SpeedData = new SpecialData(baseAbility, "travel_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

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

        public override float CastRange
        {
            get
            {
                //todo delete after fixing prediction range
                return Math.Min(this.Range - this.Radius, base.CastRange);
            }
        }

        public string ImmobilityModifierName { get; } = "modifier_invoker_tornado";

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

        public AbilityId[] RequiredOrbs { get; } = { AbilityId.invoker_wex, AbilityId.invoker_wex, AbilityId.invoker_quas };

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.invokeHelper.CanInvoke(!this.IsInvoked);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.invokeHelper.Wex.Level)
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