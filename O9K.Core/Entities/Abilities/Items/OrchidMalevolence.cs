namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_orchid)]
    public class OrchidMalevolence : RangedAbility, /*IHasDamageAmplify,*/ IDisable
    {
        private readonly SpecialData amplifierData;

        public OrchidMalevolence(Ability baseAbility)
            : base(baseAbility)
        {
            //todo enable amplifier ?

            this.amplifierData = new SpecialData(baseAbility, "silence_damage_percent");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string AmplifierModifierName { get; } = "modifier_orchid_malevolence_debuff";

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / 100;
        }
    }
}