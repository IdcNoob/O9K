namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
    using System;

    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using SharpDX;

    [AbilityId(AbilityId.storm_spirit_ball_lightning)]
    public class BallLightning : LineAbility, IBlink
    {
        private readonly SpecialData travelCostBase;

        private readonly SpecialData travelCostPercent;

        public BallLightning(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "ball_lightning_move_speed");
            this.RadiusData = new SpecialData(baseAbility, "ball_lightning_aoe");
            this.travelCostBase = new SpecialData(baseAbility, "ball_lightning_travel_cost_base");
            this.travelCostPercent = new SpecialData(baseAbility, "ball_lightning_travel_cost_percent");
        }

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override float CastRange { get; } = 600; //hack for auto usage

        public float MaxCastRange
        {
            get
            {
                var mana = this.Owner.Mana - this.ManaCost;
                return (float)Math.Ceiling(mana / this.TravelCost) * 100;
            }
        }

        private float TravelCost
        {
            get
            {
                return this.travelCostBase.GetValue(this.Level)
                       + (this.Owner.MaximumMana * (this.travelCostPercent.GetValue(this.Level) / 100));
            }
        }

        public int GetRemainingMana(Vector3 position)
        {
            return (int)(this.Owner.Mana - this.GetRequiredMana(position));
        }

        public int GetRequiredMana(Vector3 position)
        {
            var distance = this.Owner.Distance(position);
            return (int)(this.ManaCost + ((float)Math.Floor(distance / 100) * this.TravelCost));
        }
    }
}