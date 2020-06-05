namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.winter_wyvern_winters_curse)]
    public class WintersCurse : RangedAreaOfEffectAbility, IHasDamageAmplify, IDisable
    {
        public WintersCurse(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix main target owner damage

            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public DamageType AmplifierDamageType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public string[] AmplifierModifierNames { get; } =
        {
            "modifier_winter_wyvern_winters_curse", "modifier_winter_wyvern_winters_curse_aura"
        };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Incoming;

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            return -1;
        }
    }
}