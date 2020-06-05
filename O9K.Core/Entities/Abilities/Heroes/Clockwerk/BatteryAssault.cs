namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rattletrap_battery_assault)]
    public class BatteryAssault : AreaOfEffectAbility, IBuff
    {
        public BatteryAssault(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string BuffModifierName { get; } = "modifier_rattletrap_battery_assault";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}