namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bloodseeker_bloodrage)]
    public class Bloodrage : RangedAbility, IHasDamageAmplify, IBuff
    {
        private readonly SpecialData amplifierData;

        public Bloodrage(Ability baseAbility)
            : base(baseAbility)
        {
            //todo different in/out dmg amp
            this.amplifierData = new SpecialData(baseAbility, "damage_increase_incoming_pct");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_bloodseeker_bloodrage" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.All;

        public string BuffModifierName { get; } = "modifier_bloodseeker_bloodrage";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / 100;
        }
    }
}