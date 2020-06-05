namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
    using System;

    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.visage_gravekeepers_cloak)]
    public class GravekeepersCloak : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        private readonly SpecialData maxDamageReductionData;

        public GravekeepersCloak(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
            this.maxDamageReductionData = new SpecialData(baseAbility, "max_damage_reduction");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_visage_gravekeepers_cloak" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (this.Level == 0)
            {
                return 0;
            }

            var amplify = (this.amplifierData.GetValue(this.Level) / -100) * target.GetModifierStacks(this.AmplifierModifierNames[0]);

            return Math.Max(amplify, this.maxDamageReductionData.GetValue(this.Level) / -100f);
        }
    }
}