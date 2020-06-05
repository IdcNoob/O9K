namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.winter_wyvern_cold_embrace)]
    public class ColdEmbrace : RangedAbility, IHasDamageAmplify, IShield
    {
        public ColdEmbrace(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical;

        public string[] AmplifierModifierNames { get; } = { "modifier_winter_wyvern_cold_embrace" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune | UnitState.Stunned;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_winter_wyvern_cold_embrace";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}