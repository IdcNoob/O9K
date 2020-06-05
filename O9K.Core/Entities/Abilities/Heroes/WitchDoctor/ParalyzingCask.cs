namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.witch_doctor_paralyzing_cask)]
    public class ParalyzingCask : RangedAbility, IDisable
    {
        public ParalyzingCask(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}