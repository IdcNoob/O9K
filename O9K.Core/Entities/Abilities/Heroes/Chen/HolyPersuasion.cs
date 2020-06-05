namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.chen_holy_persuasion)]
    public class HolyPersuasion : RangedAbility, IShield
    {
        public HolyPersuasion(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_chen_test_of_faith_teleport";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;

        public override bool TargetsEnemy { get; } = false;
    }
}