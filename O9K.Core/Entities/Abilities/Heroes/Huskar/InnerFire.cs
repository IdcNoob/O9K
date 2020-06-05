namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.huskar_inner_fire)]
    public class InnerFire : AreaOfEffectAbility, IDisable, INuke
    {
        public InnerFire(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Disarmed;
    }
}