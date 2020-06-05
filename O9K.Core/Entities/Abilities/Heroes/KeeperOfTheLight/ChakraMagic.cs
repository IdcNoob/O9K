namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.keeper_of_the_light_chakra_magic)]
    public class ChakraMagic : RangedAbility, IManaRestore
    {
        private readonly SpecialData manaRestoreData;

        public ChakraMagic(Ability baseAbility)
            : base(baseAbility)
        {
            this.manaRestoreData = new SpecialData(baseAbility, "mana_restore");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetManaRestore(Unit9 unit)
        {
            return (int)this.manaRestoreData.GetValue(this.Level);
        }
    }
}