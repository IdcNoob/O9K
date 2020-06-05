namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_crimson_guard)]
    public class CrimsonGuard : AreaOfEffectAbility, IShield
    {
        public CrimsonGuard(Ability baseAbility)
            : base(baseAbility)
        {
            //todo damage block?
            this.RadiusData = new SpecialData(baseAbility, "bonus_aoe_radius");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_item_crimson_guard_nostack";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}