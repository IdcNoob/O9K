namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.centaur_stampede)]
    public class Stampede : ActiveAbility, IHasDamageAmplify
    {
        private readonly SpecialData amplifierData;

        public Stampede(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_centaur_stampede" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.Owner.HasAghanimsScepter)
            {
                return 0;
            }

            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}