namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.item_iron_talon)]
    public class IronTalon : RangedAbility
    {
        public IronTalon(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "creep_damage_pct");
        }

        public override DamageType DamageType { get; } = DamageType.Pure;

        public override bool TargetsEnemy { get; } = false;

        protected override float BaseCastRange
        {
            get
            {
                return base.BaseCastRange + 100;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();
            if (!unit.IsCreep)
            {
                return damage;
            }

            damage[this.DamageType] = unit.Health * (this.DamageData.GetValue(this.Level) / 100f);

            return damage;
        }
    }
}