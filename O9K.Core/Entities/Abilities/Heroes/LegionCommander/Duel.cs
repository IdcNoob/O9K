namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.legion_commander_duel)]
    public class Duel : RangedAbility, IHasDamageAmplify, IDisable, IAppliesImmobility
    {
        public Duel(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_legion_commander_duel" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ImmobilityModifierName { get; } = "modifier_legion_commander_duel";

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        protected override float BaseCastRange
        {
            get
            {
                return base.BaseCastRange + 100;
            }
        }

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.Owner.HasAghanimsScepter)
            {
                return 0;
            }

            return -1;
        }
    }
}