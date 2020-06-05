namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.axe_battle_hunger)]
    public class BattleHunger : RangedAbility, IHasDamageAmplify, IDebuff
    {
        private readonly SpecialData amplifierData;

        public BattleHunger(Ability baseAbility)
            : base(baseAbility)
        {
            this.amplifierData = new SpecialData(baseAbility, "damage_reduction_scepter");
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

        public string[] AmplifierModifierNames { get; } = { "modifier_axe_battle_hunger" };

        public AmplifiesDamage AmplifiesDamage { get; } = AmplifiesDamage.Outgoing;

        public string DebuffModifierName { get; } = "modifier_axe_battle_hunger";

        public bool IsAmplifierAddedToStats { get; } = false;

        public bool IsAmplifierPermanent { get; } = false;

        public float AmplifierValue(Unit9 source, Unit9 target)
        {
            if (!this.Owner.HasAghanimsScepter)
            {
                return 0;
            }

            return this.amplifierData.GetValue(this.Level) / -100;
        }
    }
}