namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bristleback_bristleback)]
    public class Bristleback : PassiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData backAmplifierData;

        private readonly SpecialData sideAmplifierData;

        public Bristleback(Ability baseAbility)
            : base(baseAbility)
        {
            this.sideAmplifierData = new SpecialData(baseAbility, "side_damage_reduction");
            this.backAmplifierData = new SpecialData(baseAbility, "back_damage_reduction");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_bristleback_bristleback" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.CanBeCasted())
            {
                return 0;
            }

            var angle = target.GetAngle(source.Position);

            if (angle <= 1.1)
            {
                return 0;
            }

            if (angle <= 1.9)
            {
                return this.sideAmplifierData.GetValue(this.Level) / -100;
            }

            return this.backAmplifierData.GetValue(this.Level) / -100;
        }
    }
}