namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_urn_of_shadows)]
    public class UrnOfShadows : RangedAbility, IHealthRestore, IDebuff
    {
        private readonly SpecialData healthRestoreData;

        public UrnOfShadows(Ability baseAbility)
            : base(baseAbility)
        {
            this.DurationData = new SpecialData(baseAbility, "duration");
            this.healthRestoreData = new SpecialData(baseAbility, "soul_heal_amount");
        }

        public override bool BreaksLinkens { get; } = false;

        public override DamageType DamageType { get; } = DamageType.Magical;

        public string DebuffModifierName { get; } = "modifier_item_urn_damage";

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_item_urn_heal";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.Charges > 0 && base.CanBeCasted(checkChanneling);
        }

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}