namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using Prediction.Collision;

    [AbilityId(AbilityId.mars_spear)]
    public class SpearOfMars : LineAbility, INuke, IDisable
    {
        public SpearOfMars(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.SpeedData = new SpecialData(baseAbility, "spear_speed");
            this.RadiusData = new SpecialData(baseAbility, "spear_width");
            this.RangeData = new SpecialData(baseAbility, "spear_range");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.EnemyHeroes;

        public override bool HasAreaOfEffect { get; } = false;

        protected override float BaseCastRange
        {
            get
            {
                return this.RangeData.GetValue(this.Level);
            }
        }
    }
}