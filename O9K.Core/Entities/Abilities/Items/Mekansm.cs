namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_mekansm)]
    public class Mekansm : AreaOfEffectAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public Mekansm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "heal_radius");
            this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = "modifier_item_mekansm_noheal";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}