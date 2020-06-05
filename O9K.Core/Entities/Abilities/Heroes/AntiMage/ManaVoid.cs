namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.antimage_mana_void)]
    public class ManaVoid : RangedAreaOfEffectAbility, INuke
    {
        public ManaVoid(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "mana_void_aoe_radius");
            this.DamageData = new SpecialData(baseAbility, "mana_void_damage_per_mana");
        }

        public override bool CanHitSpellImmuneEnemy { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var manaDamage = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = (int)((unit.MaximumMana - unit.Mana) * manaDamage)
            };
        }
    }
}