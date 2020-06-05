namespace O9K.Core.Entities.Abilities.Heroes.Snapfire
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    using SharpDX;

    [AbilityId(AbilityId.snapfire_firesnap_cookie)]
    public class FiresnapCookie : AreaOfEffectAbility, IDisable, INuke
    {
        private readonly SpecialData cookieSpeedData;

        private readonly SpecialData jumpRangeData;

        public FiresnapCookie(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "jump_duration");
            this.RadiusData = new SpecialData(baseAbility, "impact_radius");
            this.DamageData = new SpecialData(baseAbility, "impact_damage");
            this.jumpRangeData = new SpecialData(baseAbility, "jump_horizontal_distance");
            this.cookieSpeedData = new SpecialData(baseAbility, "projectile_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float CookieSpeed
        {
            get
            {
                return this.cookieSpeedData.GetValue(this.Level);
            }
        }

        public float JumpRange
        {
            get
            {
                return this.jumpRangeData.GetValue(this.Level);
            }
        }

        public override float Range
        {
            get
            {
                return this.jumpRangeData.GetValue(this.Level) + this.Radius;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.BaseAbility.CastRange;
            }
        }

        public override float GetCastDelay(Vector3 position)
        {
            return this.GetCastDelay() + this.Owner.GetTurnTime(position) + (this.Owner.Distance(position) / this.CookieSpeed);
        }

        public override PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = base.GetPredictionInput(target, aoeTargets);
            input.CastRange = this.JumpRange;

            return input;
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            //todo fix ?
            var result = this.BaseAbility.UseAbility(this.Owner, queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}