namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.riki_smoke_screen)]
    public class SmokeScreen : CircleAbility, IDisable
    {
        public SmokeScreen(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;
    }
}