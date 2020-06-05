namespace O9K.Core.Entities.Abilities.Heroes.Luna
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.luna_lucent_beam)]
    public class LucentBeam : RangedAbility, INuke, IDisable
    {
        public LucentBeam(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "beam_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}