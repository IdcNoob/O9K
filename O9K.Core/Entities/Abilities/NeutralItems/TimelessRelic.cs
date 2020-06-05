namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_timeless_relic)]
    public class TimelessRelic : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public TimelessRelic(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "spell_amp");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_item_timeless_relic" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.IsUsable)
            {
                return 0;
            }

            return this.amplifierData.GetValue(this.Level) / 100f;
        }
    }
}