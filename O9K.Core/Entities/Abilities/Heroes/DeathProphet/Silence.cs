namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.death_prophet_silence)]
    public class Silence : CircleAbility, IDisable
    {
        public Silence(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;
    }
}