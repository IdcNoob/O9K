namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_greater_faerie_fire)]
    public class GreaterFaerieFire : ActiveAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public GreaterFaerieFire(Ability baseAbility)
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