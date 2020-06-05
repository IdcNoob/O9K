namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.dazzle_shallow_grave)]
    public class ShallowGrave : RangedAbility, IHasDamageAmplify, IShield
    {
        public ShallowGrave(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_dazzle_shallow_grave" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_dazzle_shallow_grave";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}