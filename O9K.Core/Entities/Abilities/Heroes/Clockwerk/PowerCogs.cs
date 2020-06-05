namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rattletrap_power_cogs)]
    public class PowerCogs : AreaOfEffectAbility
    {
        private readonly SpecialData triggerRadiusData;

        public PowerCogs(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "cogs_radius");
            this.triggerRadiusData = new SpecialData(baseAbility, "trigger_distance");
        }

        public override float ActivationDelay { get; } = 0.1f;

        public override float Radius

        {
            get
            {
                return (this.RadiusData.GetValue(this.Level) + this.triggerRadiusData.GetValue(this.Level)) - 25;
            }
        }
    }
}