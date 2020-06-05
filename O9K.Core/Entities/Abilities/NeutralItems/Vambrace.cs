namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_vambrace)]
    public class Vambrace : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public Vambrace(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "bonus_spell_amp");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_item_vambrace" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.IsUsable || this.Owner.PrimaryAttribute != Attribute.Intelligence)
            {
                return 0;
            }

            return this.amplifierData.GetValue(this.Level) / 100f;
        }
    }
}