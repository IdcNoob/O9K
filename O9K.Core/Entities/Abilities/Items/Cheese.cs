namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_cheese)]
    public class Cheese : ActiveAbility, IHealthRestore, IManaRestore
    {
        private readonly SpecialData healthRestoreData;

        private readonly SpecialData manaRestoreData;

        public Cheese(Ability baseAbility)
            : base(baseAbility)
        {
            this.healthRestoreData = new SpecialData(baseAbility, "health_restore");
            this.manaRestoreData = new SpecialData(baseAbility, "mana_restore");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = false;

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