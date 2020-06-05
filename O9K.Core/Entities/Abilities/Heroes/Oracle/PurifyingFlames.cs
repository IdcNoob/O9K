namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.oracle_purifying_flames)]
    public class PurifyingFlames : RangedAbility, INuke, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public PurifyingFlames(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.DurationData = new SpecialData(baseAbility, "duration");
            this.healthRestoreData = new SpecialData(baseAbility, "heal_per_second");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}