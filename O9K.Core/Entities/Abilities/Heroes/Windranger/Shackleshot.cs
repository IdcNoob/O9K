namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.windrunner_shackleshot)]
    public class Shackleshot : RangedAbility, IDisable
    {
        private readonly SpecialData angleData;

        public Shackleshot(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
            this.RangeData = new SpecialData(baseAbility, "shackle_distance");
            this.angleData = new SpecialData(baseAbility, "shackle_angle");
        }

        public float Angle
        {
            get
            {
                return this.angleData.GetValue(this.Level);
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Range
        {
            get
            {
                return base.Range + this.ShackleRange;
            }
        }

        public float ShackleRange
        {
            get
            {
                return this.RangeData.GetValue(this.Level) - 100;
            }
        }
    }
}