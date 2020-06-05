namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.necrolyte_death_pulse)]
    public class DeathPulse : AreaOfEffectAbility, IHealthRestore, INuke
    {
        private readonly SpecialData healthRestoreData;

        public DeathPulse(Ability baseAbility)
            : base(baseAbility)
        {
            //todo add talent heal
            this.RadiusData = new SpecialData(baseAbility, "area_of_effect");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.healthRestoreData = new SpecialData(baseAbility, "heal");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}