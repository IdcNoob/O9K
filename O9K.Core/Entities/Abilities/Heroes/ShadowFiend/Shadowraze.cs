namespace O9K.Core.Entities.Abilities.Heroes.ShadowFiend
{
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.nevermore_shadowraze1)]
    [AbilityId(AbilityId.nevermore_shadowraze2)]
    [AbilityId(AbilityId.nevermore_shadowraze3)]
    public class Shadowraze : AreaOfEffectAbility, INuke
    {
        private readonly SpecialData bonusDamageData;

        private readonly SpecialData castRangeData;

        public Shadowraze(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "shadowraze_radius");
            this.DamageData = new SpecialData(baseAbility, "shadowraze_damage");
            this.castRangeData = new SpecialData(baseAbility, "shadowraze_range");
            this.bonusDamageData = new SpecialData(baseAbility, "stack_bonus_damage");
        }

        public override float CastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var stacks = unit.BaseModifiers.FirstOrDefault(x => x.Name == "modifier_nevermore_shadowraze_debuff")?.StackCount ?? 0;
            var bonus = this.bonusDamageData.GetValue(this.Level);

            damage[this.DamageType] += stacks * bonus;

            return damage;
        }
    }
}