namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_essence_ring)]
    public class EssenceRing : ActiveAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public EssenceRing(Ability baseAbility)
            : base(baseAbility)
        {
            this.healthRestoreData = new SpecialData(baseAbility, "health_gain");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = "modifier_item_essence_ring_active";

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}