namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.antimage_counterspell)]
    public class Counterspell : ActiveAbility, IShield
    {
        public Counterspell(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_antimage_counterspell";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}