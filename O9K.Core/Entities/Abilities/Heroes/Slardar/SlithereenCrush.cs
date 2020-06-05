namespace O9K.Core.Entities.Abilities.Heroes.Slardar
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.slardar_slithereen_crush)]
    public class SlithereenCrush : AreaOfEffectAbility, IDisable, INuke
    {
        public SlithereenCrush(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "crush_radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}