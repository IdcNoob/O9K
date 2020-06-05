namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.item_quelling_blade)]
    public class QuellingBlade : RangedAbility, IHasPassiveDamageIncrease
    {
        private readonly SpecialData rangedDamageData;

        public QuellingBlade(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_bonus");
            this.rangedDamageData = new SpecialData(baseAbility, "damage_bonus_ranged");
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public bool IsPassiveDamagePermanent { get; } = true;

        public bool MultipliedByCrit { get; } = false;

        public string PassiveDamageModifierName { get; } = string.Empty;

        public override bool TargetsEnemy { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (unit.IsCreep && this.IsUsable && !unit.IsAlly(this.Owner) && !this.Owner.IsIllusion)
            {
                damage[this.DamageType] =
                    this.Owner.IsRanged ? this.rangedDamageData.GetValue(this.Level) : this.DamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}