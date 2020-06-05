namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.juggernaut_healing_ward)]
    public class HealingWard : CircleAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public HealingWard(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "healing_ward_aura_radius");
            this.healthRestoreData = new SpecialData(baseAbility, "healing_ward_heal_amount");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; set; }

        public bool RestoresOwner { get; set; }

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)((this.healthRestoreData.GetValue(this.Level) / 100f) * unit.MaximumHealth * this.Duration);
        }
    }
}