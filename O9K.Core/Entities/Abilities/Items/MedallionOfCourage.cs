namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_medallion_of_courage)]
    public class MedallionOfCourage : RangedAbility, IShield, IDebuff
    {
        public MedallionOfCourage(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public override bool BreaksLinkens { get; } = false;

        public string DebuffModifierName { get; } = "modifier_item_medallion_of_courage_armor_reduction";

        public string ShieldModifierName { get; } = "modifier_item_medallion_of_courage_armor_addition";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;
    }
}