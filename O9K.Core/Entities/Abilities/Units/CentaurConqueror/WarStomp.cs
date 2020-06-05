namespace O9K.Core.Entities.Abilities.Units.CentaurConqueror
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.centaur_khan_war_stomp)]
    public class WarStomp : AreaOfEffectAbility, IDisable
    {
        public WarStomp(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}