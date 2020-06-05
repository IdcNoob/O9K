namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.item_aeon_disk)]
    public class AeonDisk : PassiveAbility, IHasDamageAmplify
    {
        public AeonDisk(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_item_aeon_disk_buff" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.All;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}