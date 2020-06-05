namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rubick_arcane_supremacy)]
    public class ArcaneSupremacy : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public ArcaneSupremacy(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "spell_amp");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_rubick_arcane_supremacy" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / 100f;
        }
    }
}