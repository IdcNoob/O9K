namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.abaddon_borrowed_time)]
    public class BorrowedTime : ActiveAbility, IHasDamageAmplify
    {
        public BorrowedTime(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_abaddon_borrowed_time" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public override bool TargetsEnemy { get; } = false;

        protected override bool CanBeCastedWhileStunned { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            //todo aghs ally reduce?
            return -1;
        }
    }
}