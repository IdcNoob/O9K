namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    using Prediction.Collision;

    [AbilityId(AbilityId.mirana_arrow)]
    public class SacredArrow : LineAbility, IDisable
    {
        public SacredArrow(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "arrow_width");
            this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.EnemyUnits;

        public bool MultipleArrows
        {
            get
            {
                return this.Owner.GetAbilityById(AbilityId.special_bonus_unique_mirana_2)?.Level > 0;
            }
        }
    }
}