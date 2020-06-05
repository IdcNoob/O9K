namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_tango)]
    [AbilityId(AbilityId.item_tango_single)]
    public class Tango : RangedAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public Tango(Ability ability)
            : base(ability)
        {
            this.DurationData = new SpecialData(ability, "buff_duration");
            this.healthRestoreData = new SpecialData(ability, "health_regen");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_tango_heal";

        public bool RestoresAlly { get; } = false;

        public bool RestoresOwner { get; } = true;

        public override bool TargetsEnemy { get; } = false;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}