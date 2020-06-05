namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
    using System;
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.dazzle_shadow_wave)]
    public class ShadowWave : RangedAreaOfEffectAbility, IHealthRestore, INuke
    {
        private readonly SpecialData damageRadius;

        private readonly SpecialData targetsData;

        public ShadowWave(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "bounce_radius");
            this.damageRadius = new SpecialData(baseAbility, "damage_radius");
            this.targetsData = new SpecialData(baseAbility, "max_targets");
        }

        public float DamageRadius
        {
            get
            {
                return this.damageRadius.GetValue(this.Level);
            }
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.DamageData.GetValue(this.Level);
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = this.DamageData.GetValue(this.Level);
            var multiplier = EntityManager9.Units.Count(
                x => x.IsUnit && x.IsVisible && x.IsAlive && x.IsEnemy(unit) && x.Distance(unit) < this.DamageRadius);

            return new Damage
            {
                [this.DamageType] = (int)(damage * Math.Max(0, Math.Min(multiplier, this.targetsData.GetValue(this.Level))))
            };
        }
    }
}