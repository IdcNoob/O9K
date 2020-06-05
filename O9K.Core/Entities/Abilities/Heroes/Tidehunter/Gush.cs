namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
    using System.Collections.Generic;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    using Prediction.Data;

    [AbilityId(AbilityId.tidehunter_gush)]
    public class Gush : RangedAbility, INuke
    {
        private readonly SpecialData scepterSpeedData;

        public Gush(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "aoe_scepter");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.DamageData = new SpecialData(baseAbility, "gush_damage");
            this.scepterSpeedData = new SpecialData(baseAbility, "speed_scepter");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public override float Radius
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.RadiusData.GetValue(this.Level);
                }

                return 0;
            }
        }

        public override float Speed
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterSpeedData.GetValue(this.Level);
                }

                return base.Speed;
            }
        }

        public override bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            if (!this.PositionCast)
            {
                return this.UseAbility(mainTarget, queue, bypass);
            }

            var input = this.GetPredictionInput(mainTarget, aoeTargets);
            var output = this.GetPredictionOutput(input);

            if (output.HitChance < minimumChance)
            {
                return false;
            }

            return this.UseAbility(output.CastPosition, queue, bypass);
        }
    }
}