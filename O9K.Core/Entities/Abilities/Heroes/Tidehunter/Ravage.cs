namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.tidehunter_ravage)]
    public class Ravage : AreaOfEffectAbility, IDisable, INuke
    {
        public Ravage(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve hit time calcs
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}