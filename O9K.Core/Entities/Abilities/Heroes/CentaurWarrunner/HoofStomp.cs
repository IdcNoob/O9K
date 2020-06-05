namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.centaur_hoof_stomp)]
    public class HoofStomp : AreaOfEffectAbility, IDisable, INuke
    {
        public HoofStomp(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "stomp_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}