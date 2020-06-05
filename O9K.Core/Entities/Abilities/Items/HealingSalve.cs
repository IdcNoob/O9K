namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_flask)]
    public class HealingSalve : RangedAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public HealingSalve(Ability ability)
            : base(ability)
        {
            this.DurationData = new SpecialData(ability, "buff_duration");
            this.healthRestoreData = new SpecialData(ability, "health_regen");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_flask_healing";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}