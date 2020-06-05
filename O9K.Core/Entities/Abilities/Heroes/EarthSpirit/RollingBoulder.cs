namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using Prediction.Collision;

    [AbilityId(AbilityId.earth_spirit_rolling_boulder)]
    public class RollingBoulder : LineAbility, IBlink, IDisable
    {
        private readonly SpecialData castRangeData;

        public RollingBoulder(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "delay");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.castRangeData = new SpecialData(baseAbility, "distance");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public BlinkType BlinkType { get; } = BlinkType.Blink;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.EnemyHeroes;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}