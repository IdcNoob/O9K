namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Metadata;

    [AbilityId(AbilityId.nyx_assassin_spiked_carapace)]
    public class SpikedCarapace : ActiveAbility, IHasDamageAmplify, IShield
    {
        public SpikedCarapace(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } = { "modifier_nyx_assassin_spiked_carapace" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = 0;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public string ShieldModifierName { get; } = "modifier_nyx_assassin_spiked_carapace";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}