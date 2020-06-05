namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_enchanted_mango)]
    public class EnchantedMango : RangedAbility, IManaRestore
    {
        private readonly SpecialData manaRestoreData;

        public EnchantedMango(Ability baseAbility)
            : base(baseAbility)
        {
            this.manaRestoreData = new SpecialData(baseAbility, "replenish_amount");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                return base.AbilityBehavior | AbilityBehavior.UnitTarget;
            }
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public override bool TargetsEnemy { get; } = false;

        public int GetManaRestore(Unit9 unit)
        {
            return (int)this.manaRestoreData.GetValue(this.Level);
        }
    }
}