namespace O9K.Core.Entities.Abilities.Heroes.WitchDoctor
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.witch_doctor_maledict)]
    public class Maledict : CircleAbility, /*IHasDamageAmplify,*/ IDebuff
    {
        private readonly SpecialData amplifierData;

        public Maledict(Ability baseAbility)
            : base(baseAbility)
        {
            //todo enable amplifier ?

            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.amplifierData = new SpecialData(baseAbility, "bonus_damage");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string AmplifierModifierName { get; } = "modifier_maledict";

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public string DebuffModifierName { get; } = "modifier_maledict";

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / 100;
        }
    }
}