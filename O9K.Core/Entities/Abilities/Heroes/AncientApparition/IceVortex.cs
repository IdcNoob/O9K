namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ancient_apparition_ice_vortex)]
    public class IceVortex : CircleAbility, IHasDamageAmplify, IDebuff
    {
        private readonly SpecialData amplifierData;

        public IceVortex(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.amplifierData = new SpecialData(baseAbility, "spell_resist_pct");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_ice_vortex" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public string DebuffModifierName { get; } = "modifier_ice_vortex";

        public bool IsAmplifierAddedToStats { get; } = true;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}