namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.pugna_decrepify)]
    public class Decrepify : RangedAbility, IHasDamageAmplify, IShield, IDebuff, IDisable
    {
        private readonly SpecialData amplifierData;

        public Decrepify(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "bonus_spell_damage_pct");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical;

        public string[] AmplifierModifierNames { get; } = { "modifier_pugna_decrepify" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.AttackImmune | UnitState.Disarmed;

        public string DebuffModifierName { get; } = "modifier_pugna_decrepify";

        public bool IsAmplifierAddedToStats { get; } = true;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_pugna_decrepify";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}