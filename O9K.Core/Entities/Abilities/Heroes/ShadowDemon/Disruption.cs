namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.shadow_demon_disruption)]
    public class Disruption : RangedAbility, IDisable, IShield, IAppliesImmobility
    {
        private bool talentLearned;

        public Disruption(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned | UnitState.Invulnerable;

        public string ImmobilityModifierName { get; } = "modifier_shadow_demon_disruption";

        public override bool IsDisplayingCharges
        {
            get
            {
                if (this.talentLearned)
                {
                    return true;
                }

                return this.talentLearned = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_shadow_demon_7)?.Level > 0;
            }
        }

        public string ShieldModifierName { get; } = "modifier_shadow_demon_disruption";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}