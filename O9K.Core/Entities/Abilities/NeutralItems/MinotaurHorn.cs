namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_minotaur_horn)]
    public class MinotaurHorn : ActiveAbility, IShield
    {
        public MinotaurHorn(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.MagicImmune;

        public string ShieldModifierName { get; } = "modifier_black_king_bar_immune";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}