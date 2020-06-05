namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ursa_enrage)]
    public class Enrage : ActiveAbility, IHasDamageAmplify, IShield
    {
        private readonly SpecialData amplifierData;

        public Enrage(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "damage_reduction");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_ursa_enrage" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = 0;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public bool ProvidesStatusResistance
        {
            get
            {
                return true;

                //var talent = this.Owner.GetAbilityById((AbilityId)7133);
                //if (talent?.Level > 0)
                //{
                //    return true;
                //}

                //return false;
            }
        }

        public string ShieldModifierName { get; } = "modifier_ursa_enrage";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public override bool TargetsEnemy { get; } = false;

        protected override bool CanBeCastedWhileStunned
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}