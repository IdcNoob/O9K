namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dark_seer_vacuum)]
    public class Vacuum : CircleAbility, IDisable, INuke
    {
        public Vacuum(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}