namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.obsidian_destroyer_astral_imprisonment)]
    public class AstralImprisonment : RangedAbility, IDisable, IShield, INuke, IAppliesImmobility
    {
        public AstralImprisonment(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned | UnitState.Invulnerable;

        public string ImmobilityModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_prison";

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        public string ShieldModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_prison";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;

        public override int GetDamage(Unit9 unit)
        {
            var healthRegen = unit.HealthRegeneration * this.Duration;
            var damage = this.DamageData.GetValue(this.Level) - healthRegen;
            var amplify = unit.GetDamageAmplification(this.Owner, this.DamageType, true);
            var block = unit.GetDamageBlock(this.DamageType);

            return (int)((damage - block) * amplify);
        }
    }
}