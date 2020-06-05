namespace O9K.Core.Entities.Abilities.Heroes.TrollWarlord
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.troll_warlord_battle_trance)]
    public class BattleTrance : ActiveAbility, IHasDamageAmplify, IShield
    {
        public BattleTrance(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return AbilityBehavior.UnitTarget;
                }

                return AbilityBehavior.NoTarget; // wrong base behavior
            }
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_troll_warlord_battle_trance" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_troll_warlord_battle_trance";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                return this.UseAbility(this.Owner, queue, bypass);
            }

            return base.UseAbility(queue, bypass);
        }
    }
}