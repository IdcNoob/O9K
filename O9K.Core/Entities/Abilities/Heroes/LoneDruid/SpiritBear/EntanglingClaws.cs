namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid.SpiritBear
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_spirit_bear_entangle)]
    public class EntanglingClaws : PassiveAbility, IAppliesImmobility
    {
        public EntanglingClaws(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string ImmobilityModifierName { get; } = "modifier_lone_druid_spirit_bear_entangle_effect";
    }
}