namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.witch_doctor_voodoo_restoration)]
    public class VoodooRestoration : ToggleAbility, IHasRadius, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public VoodooRestoration(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.healthRestoreData = new SpecialData(baseAbility, "heal");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_voodoo_restoration_heal";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}