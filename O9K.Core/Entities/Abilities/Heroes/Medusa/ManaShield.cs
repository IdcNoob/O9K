namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.medusa_mana_shield)]
    public class ManaShield : ToggleAbility, IHasDamageAmplify, IShield
    {
        private readonly SpecialData amplifierData;

        public ManaShield(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve amp calcs

            this.amplifierData = new SpecialData(baseAbility, "absorption_pct");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_medusa_mana_shield" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = 0;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_medusa_mana_shield";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}