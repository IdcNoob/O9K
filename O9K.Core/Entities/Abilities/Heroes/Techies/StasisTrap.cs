namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
    using Base;
    using Base.Components;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.techies_stasis_trap)]
    public class StasisTrap : CircleAbility, IAppliesImmobility
    {
        public StasisTrap(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "activation_radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "activation_time");
        }

        public string ImmobilityModifierName { get; } = "modifier_techies_stasis_trap_stunned";
    }
}