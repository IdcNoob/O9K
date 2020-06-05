namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spectre_dispersion)]
    public class Dispersion : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public Dispersion(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "damage_reflection_pct");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_spectre_dispersion" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}