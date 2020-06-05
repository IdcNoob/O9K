namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.batrider_flaming_lasso)]
    public class FlamingLasso : RangedAbility, IDisable
    {
        public FlamingLasso(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }
    }
}