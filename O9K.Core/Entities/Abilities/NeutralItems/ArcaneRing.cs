namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_arcane_ring)]
    public class ArcaneRing : AreaOfEffectAbility, IManaRestore
    {
        private readonly SpecialData manaRestoreData;

        public ArcaneRing(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
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