namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_ghost)]
    public class GhostScepter : ActiveAbility, IShield
    {
        public GhostScepter(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune | UnitState.Disarmed;

        public string ShieldModifierName { get; } = "modifier_ghost_state";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}