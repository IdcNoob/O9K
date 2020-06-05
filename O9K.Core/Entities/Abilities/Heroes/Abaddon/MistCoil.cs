namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.abaddon_death_coil)]
    public class MistCoil : RangedAbility, IHealthRestore, INuke
    {
        private readonly SpecialData healthCostData;

        private readonly SpecialData healthRestoreData;

        public MistCoil(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "missile_speed");
            this.DamageData = new SpecialData(baseAbility, "target_damage");
            this.healthCostData = new SpecialData(baseAbility, "self_damage");
            this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = false;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}