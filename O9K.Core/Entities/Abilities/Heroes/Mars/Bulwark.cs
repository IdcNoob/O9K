namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.mars_bulwark)]
    public class Bulwark : ToggleAbility, IHasDamageAmplify
    {
        private readonly SpecialData frontAmplifierData;

        private readonly SpecialData sideAmplifierData;

        public Bulwark(Ability baseAbility)
            : base(baseAbility)
        {
            this.sideAmplifierData = new SpecialData(baseAbility, "physical_damage_reduction_side");
            this.frontAmplifierData = new SpecialData(baseAbility, "physical_damage_reduction");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical;

        public string[] AmplifierModifierNames { get; } = { "modifier_mars_bulwark" };

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
                return this.frontAmplifierData.GetValue(this.Level) / -100;
            }

            if (angle <= 1.9)
            {
                return this.sideAmplifierData.GetValue(this.Level) / -100;
            }

            return 0;
        }
    }
}