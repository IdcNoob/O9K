namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_magic_stick)]
    [AbilityId(AbilityId.item_magic_wand)]
    [AbilityId(AbilityId.item_holy_locket)]
    public class MagicWand : ActiveAbility, IHealthRestore, IManaRestore
    {
        private readonly SpecialData restoreData;

        public MagicWand(Ability baseAbility)
            : base(baseAbility)
        {
            this.restoreData = new SpecialData(baseAbility, "restore_per_charge");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.Charges > 0 && base.CanBeCasted(checkChanneling);
        }

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.Charges * this.restoreData.GetValue(this.Level));
        }

        public int GetManaRestore(Unit9 unit)
        {
            return (int)(this.Charges * this.restoreData.GetValue(this.Level));
        }
    }
}