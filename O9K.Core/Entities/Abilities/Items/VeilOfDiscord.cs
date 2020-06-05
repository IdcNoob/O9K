namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_veil_of_discord)]
    public class VeilOfDiscord : CircleAbility, IDebuff, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public VeilOfDiscord(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "debuff_radius");
            this.amplifierData = new SpecialData(baseAbility, "spell_amp");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_item_veil_of_discord_debuff" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public string DebuffModifierName { get; } = "modifier_item_veil_of_discord_debuff";

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / 100;
        }
    }
}