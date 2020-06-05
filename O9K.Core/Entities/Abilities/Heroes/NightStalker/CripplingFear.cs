namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.night_stalker_crippling_fear)]
    public class CripplingFear : AreaOfEffectAbility, IDisable
    {
        public CripplingFear(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;
    }
}