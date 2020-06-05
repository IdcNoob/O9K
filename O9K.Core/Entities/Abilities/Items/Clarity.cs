namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_clarity)]
    public class Clarity : RangedAbility, IManaRestore
    {
        private readonly SpecialData manaRestoreData;

        public Clarity(Ability baseAbility)
            : base(baseAbility)
        {
            this.DurationData = new SpecialData(baseAbility, "buff_duration");
            this.manaRestoreData = new SpecialData(baseAbility, "mana_regen");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_clarity_potion";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetManaRestore(Unit9 unit)
        {
            return (int)(this.manaRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}