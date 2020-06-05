namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_infused_raindrop)]
    public class InfusedRaindrop : PassiveAbility, IHasDamageBlock
    {
        private readonly SpecialData blockData;

        public InfusedRaindrop(Ability baseAbility)
            : base(baseAbility)
        {
            this.blockData = new SpecialData(baseAbility, "magic_damage_block");
        }

        public DamageType BlockDamageType { get; } = DamageType.Magical;

        public string BlockModifierName { get; } = "modifier_item_infused_raindrop";

        public bool BlocksDamageAfterReduction { get; } = false;

        public bool IsDamageBlockPermanent { get; } = true;

        public float BlockValue(Unit9 target)
        {
            if (!this.CanBeCasted())
            {
                return 0;
            }

            return this.blockData.GetValue(this.Level);
        }
    }
}