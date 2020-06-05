namespace O9K.Core.Entities.Abilities.Heroes.Omniknight
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.omniknight_guardian_angel)]
    public class GuardianAngel : AreaOfEffectAbility, IHasDamageAmplify, IShield
    {
        public GuardianAngel(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical;

        public string[] AmplifierModifierNames { get; } = { "modifier_omninight_guardian_angel" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_omninight_guardian_angel";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public override bool TargetsEnemy { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}