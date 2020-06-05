namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dark_willow_terrorize)]
    public class Terrorize : CircleAbility, IDisable
    {
        public Terrorize(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "destination_radius");
            this.SpeedData = new SpecialData(baseAbility, "destination_travel_speed");
            //this.ActivationDelayData = new SpecialData(baseAbility, "initial_delay");
            //todo add rubick activation delay?
        }

        public UnitState AppliesUnitState { get; } = UnitState.Disarmed | UnitState.Silenced | UnitState.Muted;
    }
}