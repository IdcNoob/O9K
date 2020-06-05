namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.necrolyte_sadist)]
    public class GhostShroud : ActiveAbility, IShield
    {
        public GhostShroud(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune | UnitState.Disarmed;

        public string ShieldModifierName { get; } = "modifier_necrolyte_sadist_active";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}