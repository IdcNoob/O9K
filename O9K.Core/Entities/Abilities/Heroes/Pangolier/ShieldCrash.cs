namespace O9K.Core.Entities.Abilities.Heroes.Pangolier
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.pangolier_shield_crash)]
    public class ShieldCrash : AreaOfEffectAbility, IHasDamageAmplify, INuke
    {
        private readonly SpecialData activationDelayThunderData;

        private readonly SpecialData castRangeData;

        private readonly SpecialData castRangeThunderData;

        public ShieldCrash(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "jump_duration");
            this.activationDelayThunderData = new SpecialData(baseAbility, "jump_duration_gyroshell");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.castRangeData = new SpecialData(baseAbility, "jump_horizontal_distance");
            this.castRangeThunderData = new SpecialData(baseAbility, "jump_height_gyroshell");
        }

        public override float ActivationDelay
        {
            get
            {
                if (this.Owner.HasModifier("modifier_pangolier_gyroshell"))
                {
                    return this.activationDelayThunderData.GetValue(this.Level);
                }

                return base.ActivationDelay;
            }
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_pangolier_shield_crash_buff" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasModifier("modifier_pangolier_gyroshell"))
                {
                    return this.castRangeThunderData.GetValue(this.Level);
                }

                return this.castRangeData.GetValue(this.Level);
            }
        }

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return target.GetModifierStacks(this.AmplifierModifierNames[0]) / -100f;
        }
    }
}