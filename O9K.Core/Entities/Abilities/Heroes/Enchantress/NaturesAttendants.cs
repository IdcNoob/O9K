namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.enchantress_natures_attendants)]
    public class NaturesAttendants : AreaOfEffectAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        private readonly SpecialData wispsCountData;

        public NaturesAttendants(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.healthRestoreData = new SpecialData(baseAbility, "heal");
            this.wispsCountData = new SpecialData(baseAbility, "wisp_count");
        }

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_enchantress_natures_attendants";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.wispsCountData.GetValue(this.Level) * this.Duration);
        }
    }
}