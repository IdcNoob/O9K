namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.shadow_demon_demonic_purge)]
    public class DemonicPurge : RangedAbility, IDebuff, IAppliesImmobility
    {
        public DemonicPurge(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_shadow_demon_purge_slow";

        public string ImmobilityModifierName { get; } = "modifier_shadow_demon_purge_slow";

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }
    }
}