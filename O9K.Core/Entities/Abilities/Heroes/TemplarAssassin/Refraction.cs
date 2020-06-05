namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_refraction)]
    public class Refraction : ActiveAbility, IHasDamageAmplify, IShield, IBuff, IHasPassiveDamageIncrease
    {
        public Refraction(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_templar_assassin_refraction_absorb" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public string BuffModifierName { get; } = "modifier_templar_assassin_refraction_damage";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public bool IsPassiveDamagePermanent { get; } = false;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = "modifier_templar_assassin_refraction_damage";

        public string ShieldModifierName { get; } = "modifier_templar_assassin_refraction_absorb";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public override bool TargetsEnemy { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (!unit.IsAlly(this.Owner))
            {
                damage[this.DamageType] = this.DamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}