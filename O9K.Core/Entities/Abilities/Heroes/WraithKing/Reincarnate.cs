namespace O9K.Core.Entities.Abilities.Heroes.WraithKing
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.skeleton_king_reincarnation)]
    public class Reincarnate : PassiveAbility, IHasDamageAmplify
    {
        public Reincarnate(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_skeleton_king_reincarnation_scepter_active" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.IsReady;
        }
    }
}