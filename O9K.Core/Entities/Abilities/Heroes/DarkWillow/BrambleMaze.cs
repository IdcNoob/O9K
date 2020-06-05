namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dark_willow_bramble_maze)]
    public class BrambleMaze : CircleAbility, IDisable, IAppliesImmobility
    {
        private readonly SpecialData additionalActivationDelayData;

        public BrambleMaze(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "placement_range");
            this.ActivationDelayData = new SpecialData(baseAbility, "initial_creation_delay");
            this.additionalActivationDelayData = new SpecialData(baseAbility, "latch_creation_delay");
        }

        public override float ActivationDelay
        {
            get
            {
                return this.ActivationDelayData.GetValue(this.Level) + this.additionalActivationDelayData.GetValue(this.Level);
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public string ImmobilityModifierName { get; } = "modifier_dark_willow_bramble_maze";
    }
}