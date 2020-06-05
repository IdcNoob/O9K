namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_guardian_greaves)]
    public class GuardianGreaves : AreaOfEffectAbility, IHealthRestore, IManaRestore
    {
        private readonly SpecialData healthRestoreData;

        private readonly SpecialData manaRestoreData;

        public GuardianGreaves(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "replenish_radius");
            this.healthRestoreData = new SpecialData(baseAbility, "replenish_health");
            this.manaRestoreData = new SpecialData(baseAbility, "replenish_mana");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = "modifier_item_mekansm_noheal";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }

        public int GetManaRestore(Unit9 unit)
        {
            return (int)this.manaRestoreData.GetValue(this.Level);
        }
    }
}