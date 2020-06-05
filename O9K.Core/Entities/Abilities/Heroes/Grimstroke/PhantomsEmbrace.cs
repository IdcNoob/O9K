namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.grimstroke_ink_creature)]
    public class PhantomsEmbrace : RangedAbility, IDisable
    {
        public PhantomsEmbrace(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;
    }
}