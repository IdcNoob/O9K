namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_soul_ring)]
    public class SoulRing : ActiveAbility, IHasHealthCost, IManaRestore
    {
        private readonly SpecialData healthCostData;

        private readonly SpecialData manaRestoreData;

        public SoulRing(Ability baseAbility)
            : base(baseAbility)
        {
            this.healthCostData = new SpecialData(baseAbility, "health_sacrifice");
            this.manaRestoreData = new SpecialData(baseAbility, "mana_gain");
        }

        public bool CanSuicide { get; } = false;

        public int HealthCost
        {
            get
            {
                return (int)this.healthCostData.GetValue(1);
            }
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public int GetManaRestore(Unit9 unit)
        {
            return (int)this.manaRestoreData.GetValue(this.Level);
        }
    }
}