namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_trident)]
    public class Trident : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public Trident(Ability baseAbility)
            : base(baseAbility)
        {
            //  this.amplifierData = new SpecialData(baseAbility, "spell_amp");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_item_trident" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.IsUsable)
            {
                return 0;
            }

            return 0.33f;
            //   return this.amplifierData.GetValue(this.Level) / 100f;
        }
    }
}