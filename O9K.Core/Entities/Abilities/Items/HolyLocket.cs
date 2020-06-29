namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_holy_locket)]
    public class HolyLocket : RangedAbility, IHealthRestore, IManaRestore
    {
        private readonly SpecialData restoreData;

        public HolyLocket(Ability baseAbility)
            : base(baseAbility)
        {
            this.restoreData = new SpecialData(baseAbility, "restore_per_charge");
        }

        public bool InstantRestore { get; } = true;

        public override bool IsDisplayingCharges { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

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