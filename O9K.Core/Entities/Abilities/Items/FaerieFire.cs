namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_faerie_fire)]
    public class FaerieFire : ActiveAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public FaerieFire(Ability baseAbility)
            : base(baseAbility)
        {
            this.healthRestoreData = new SpecialData(baseAbility, "hp_restore");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}