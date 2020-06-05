namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_solar_crest)]
    public class SolarCrest : RangedAbility, IShield, IDebuff
    {
        public SolarCrest(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public override bool BreaksLinkens { get; } = false;

        public string DebuffModifierName { get; } = "modifier_item_solar_crest_armor_reduction";

        public string ShieldModifierName { get; } = "modifier_item_solar_crest_armor_addition";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;
    }
}