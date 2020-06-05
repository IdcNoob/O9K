namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_savage_roar)]
    [AbilityId(AbilityId.lone_druid_savage_roar_bear)]
    public class SavageRoar : AreaOfEffectAbility, IDisable
    {
        public SavageRoar(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Disarmed | UnitState.Silenced | UnitState.Muted;
    }
}