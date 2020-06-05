namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.skywrath_mage_ancient_seal)]
    public class AncientSeal : RangedAbility, IHasDamageAmplify, IDisable, IDebuff
    {
        private readonly SpecialData amplifierData;

        public AncientSeal(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "resist_debuff");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_skywrath_mage_ancient_seal" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public string DebuffModifierName { get; } = "modifier_skywrath_mage_ancient_seal";

        public bool IsAmplifierAddedToStats { get; } = true;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}