namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.oracle_false_promise)]
    public class FalsePromise : RangedAbility, IHasDamageAmplify, IShield
    {
        private bool isInvisibility;

        public FalsePromise(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_oracle_false_promise_timer" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public override bool IsInvisibility
        {
            get
            {
                if (this.isInvisibility)
                {
                    return true;
                }

                return this.isInvisibility = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_oracle_4)?.Level > 0;
            }
        }

        public string ShieldModifierName { get; } = "modifier_oracle_false_promise_timer";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}